using AutoMapper;
using AutoMapper.QueryableExtensions;
using LexicalRes.Data;
using LexicalRes.Models.DTOs;
using LexicalRes.Models.Entities;
using LexicalRes.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LexicalRes.Services
{
    public class LearnService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LearnService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<LearnViewModel> GetLearnViewModel(string userId)
        {
            var viewModel = new LearnViewModel();

            viewModel.PredefinedLists = await _appDbContext.WordLists
                .ProjectTo<WordListInfoViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .ToListAsync();

            foreach (var wordList in viewModel.PredefinedLists)
            {
                wordList.LearnedCount = await GetLearnedCount(wordList.Id, userId);
            }

            viewModel.LearningCount = await _appDbContext.Scores
                    .Where(x => x.UserId == userId && x.Value < 3)
                    .CountAsync();

            viewModel.LearnedCount = await _appDbContext.Scores
                    .Where(x => x.UserId == userId && x.Value == 3)
                    .CountAsync();

            return viewModel;

        }

        public async Task<WordListDetailedViewModel> GetWordList(int wordListId, string userId)
        {
            var wordList = await _appDbContext.WordLists
                    .Include(x => x.Words)
                    .ProjectTo<WordListDetailedViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == wordListId);

            var wordIds = wordList.Words.Select(x => x.Id).ToList();

            var learnedWordIds = await _appDbContext.Scores
                .Where(x => x.UserId == userId && wordIds.Contains(x.WordId) && x.Value == 3)
                .Select(x => x.Word.Id).ToListAsync();

            wordList.Words = wordList.Words.Where(word => !learnedWordIds.Contains(word.Id)).ToList();

            wordList.WordCount = wordIds.Count;
            wordList.LearnedCount = learnedWordIds.Count;

            return wordList;
        }

        public async Task<WordListDetailedViewModel> GetLearningWordList(string userId)
        {
            var learningList = new WordListDetailedViewModel();

            learningList.LearnedCount = 0;
            learningList.Name = "Learning";
            learningList.Words = await _appDbContext.Scores
                .Where(x => x.UserId == userId && x.Value < 3)
                .Select(x => x.Word)
                .ProjectTo<WordViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            learningList.WordCount = learningList.Words.Count;

            return learningList;
        }

        public async Task<WordListDetailedViewModel> GetLearnedWordList(string userId)
        {
            var learnedList = new WordListDetailedViewModel();

            learnedList.Name = "Learning";
            learnedList.Words = await _appDbContext.Scores
                .Where(x => x.UserId == userId && x.Value == 3)
                .Select(x => x.Word)
                .ProjectTo<WordViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            learnedList.WordCount = learnedList.Words.Count;
            learnedList.LearnedCount = learnedList.Words.Count;

            return learnedList;
        }

        public async Task<int> UpdateScore(ScoreUpdateDTO dto, string userId)
        {
            var scoreExists = await _appDbContext.Scores.AnyAsync(x => x.UserId == userId && x.WordId == dto.WordId);
            var scoreValue = 0;

            if (!scoreExists)
            {
                scoreValue = Math.Max(0, dto.Delta);
                var score = new Score { UserId = userId, WordId = dto.WordId, Value = scoreValue };
                _appDbContext.Scores.Add(score);
            }
            else
            {
                var score = await _appDbContext.Scores.FirstOrDefaultAsync(x => x.UserId == userId && x.WordId == dto.WordId);
                scoreValue = Math.Max(1, Math.Min(3, score.Value + dto.Delta));
                score.Value = scoreValue;
                _appDbContext.Scores.Update(score);
            }

            await _appDbContext.SaveChangesAsync();

            return scoreValue;
        }

        public async Task<int> GetLearnedCount(int wordListId, string userId)
        {
            var wordIds = await _appDbContext.WordLists
                .Where(x => x.Id == wordListId)
                .SelectMany(x => x.Words.Select(x => x.Id))
                .ToListAsync();

            var learnedWordsCount = await _appDbContext.Scores
                    .Where(x => x.UserId == userId && wordIds.Contains(x.WordId) && x.Value == 3)
                    .CountAsync();

            return learnedWordsCount;
        }
    }
}
