using AutoMapper;
using Core.Data;
using Core.Models;
using Core.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FeedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FeedService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FeedViewModel> GetFeedData(string UserId)
        {

            UserType userType = await _context.Users
                                  .Where(U => U.UserId == UserId)
                                  .Select(U => U.UserType)
                                  .FirstOrDefaultAsync();

            int probLimit = 1500;

            if (userType == UserType.User)
            {
                probLimit = (int)(Math.Ceiling((_context.Solved.Where(S => S.UserId == UserId).ToList().Count + 1) / 7.0) * 10);
            }

            var FeedData = new FeedViewModel()
            {
                UserId = UserId,
                Name = _context.Users.Where(User => User.UserId == UserId)
                                     .Select(User => User.Name)
                                     .First()
                                     .ToString(),
                Problems = new List<FeedProblemViewModel>()
            };

            var Problems = await _context.Problems.Take(probLimit).ToListAsync();
            Problems.AddRange(await _context.Solved.Where(x => x.UserId == UserId).Select(x => x.Problem).ToListAsync());
            Problems = Problems.Distinct().ToList();

            var SolvedByUser = await _context.Solved
                                             .Where(x => x.UserId == UserId)
                                             .Select(x => x.ProblemId)
                                             .ToListAsync();

            Problems.ForEach(Problem =>
            {
                var FeedProblem = _mapper.Map<FeedProblemViewModel>(Problem);
                FeedProblem.IsSolved = SolvedByUser.Contains(Problem.ProblemId);
                FeedData.Problems.Add(FeedProblem);
            });

            FeedData.SolvedCount = SolvedByUser.Count();

            FeedData.Rank = await _context.Users
                                          .Include(x => x.Solved)
                                          .CountAsync(x => x.Solved.Count > SolvedByUser.Count) + 1;

            return FeedData;
        }


    }
}
