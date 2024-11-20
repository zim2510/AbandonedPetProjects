using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Data;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Core.Models.ViewModels;
using AutoMapper;
using Core.Services;

namespace Core.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly FeedService _feedService;

        private FeedViewModel Feed;
        List<RankViewModel> RankViewModels = new List<RankViewModel>();
        string UserId;

        public FeedController(AppDbContext context, IMapper mapper, FeedService feedService)
        {
            _context = context;
            _mapper = mapper;
            _feedService = feedService;
        }


        // GET: Problems
        public async Task<IActionResult> Index()
        {
            if (UserId == null)
            {
                UserId = User.Identity.Name;
            }

            ViewBag.Username = User.Identity.Name;

            Feed = await _feedService.GetFeedData(UserId);
            return View(Feed);
        }
        public async Task<IActionResult> Rank()
        {
            ViewBag.Username = User.Identity.Name;

            var tmpList = await _context.Users
                                        .Include(x => x.Solved)
                                        .Select(x => new { x.UserId, x.Name, SolveCount = x.Solved.Count() })
                                        .Where(x => x.SolveCount > 0)
                                        .OrderByDescending(x => x.SolveCount).ToListAsync();

            for (int i = 0; i < tmpList.Count; i++)
            {
                RankViewModel tmp = new RankViewModel()
                {
                    UserId = tmpList[i].UserId,
                    Name = tmpList[i].Name,
                    SolveCount = tmpList[i].SolveCount
                };

                RankViewModels.Add(tmp);
            }

            return View(RankViewModels);
        }

        public async Task<IActionResult> ChangeSolveStatus(string UserId, int ProblemId, bool IsSolved)
        {
            try
            {
                var x = await _context.Solved.FirstOrDefaultAsync(Solve => Solve.ProblemId == ProblemId && Solve.UserId == UserId);

                if (x == null && IsSolved)
                {
                    _context.Solved.Add(new Solved { ProblemId = ProblemId, UserId = UserId });
                }
                if (x != null && !IsSolved)
                {
                    _context.Solved.Remove(x);
                }

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Username = User.Identity.Name;

            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems.FirstOrDefaultAsync(m => m.ProblemId == id);
            ViewData["SolveCount"] = _context.Solved.Where(S => S.ProblemId == id).ToList().Count;

            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

    }
}
