using AutoMapper;
using AutoMapper.QueryableExtensions;
using LexicalRes.Data;
using LexicalRes.Models.DTOs;
using LexicalRes.Models.Entities;
using LexicalRes.Models.ViewModels;
using LexicalRes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LexicalRes.Controllers
{
    [Authorize]
    public class LearnController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly LearnService _learnService;
        private AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LearnController(UserManager<AppUser> userManager, LearnService learnService, AppDbContext appDbContext, IMapper mapper)
        {
            _userManager = userManager;
            _learnService = learnService;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var viewModel = await _learnService.GetLearnViewModel(userId);
                return View(viewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Route("/Play")]
        public async Task<IActionResult> PlayAsync(int wordListId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var viewModel = await _learnService.GetWordList(wordListId, userId);
                return View(viewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> Learning()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var viewModel = await _learnService.GetLearningWordList(userId);
                return View("/Views/Learn/Play.cshtml", viewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> Learned()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var viewModel = await _learnService.GetLearnedWordList(userId);
                return View("/Views/Learn/Play.cshtml", viewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> WordCard(int wordId)
        {
            try
            {
                var meanings = await _appDbContext.Meanings
                    .Include(x => x.Examples)
                    .Where(x => x.WordId == wordId)
                    .ProjectTo<MeaningViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return PartialView("/Views/Learn/_WordCardPartial.cshtml", meanings);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateScore([FromBody] ScoreUpdateDTO dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var newScore = await _learnService.UpdateScore(dto, userId);
                return Ok(newScore);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
