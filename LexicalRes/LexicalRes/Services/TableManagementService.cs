using AutoMapper;
using AutoMapper.QueryableExtensions;
using LexicalRes.Data;
using LexicalRes.Models.Entities;
using LexicalRes.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexicalRes.Services
{
    public class TableManagementService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TableManagementService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<WordGridModel[]> GetWordGridModels()
        {
            var words = await _appDbContext.Words
                .Include(x => x.WordLists)
                .ProjectTo<WordGridModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return words;
        }

        public async Task UpdateWord(WordGridModel viewModel)
        {
            var word = await _appDbContext.Words
                .Include(x => x.WordLists)
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            _appDbContext.RemoveRange(word.WordLists);

            _mapper.Map(viewModel, word);

            word.WordLists = await _appDbContext.WordLists
                .Where(x => viewModel.WordListIds.Contains(x.Id))
                .ToListAsync();

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<WordListInfoViewModel[]> GetWordListDTos()
        {
            var wordLists = await _appDbContext.WordLists
                .ProjectTo<WordListInfoViewModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return wordLists;
        }

        public async Task<WordListGridModel[]> GetWordListGridModels()
        {
            var wordLists = await _appDbContext.WordLists
                .ProjectTo<WordListGridModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return wordLists;
        }

        public async Task UpdateWordList(WordListGridModel viewModel)
        {
            var wordList = await _appDbContext.WordLists
                .Include(x => x.Words)
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            _appDbContext.WordWordLists
                .RemoveRange(_appDbContext.WordWordLists.Where(x => x.WordListId == wordList.Id));

            _mapper.Map(viewModel, wordList);

            var wordIds = string.IsNullOrEmpty(viewModel.WordIds) ?
                null : viewModel.WordIds.Split(',').Select(x => int.Parse(x)).ToList();

            wordList.Words = await _appDbContext.Words
                .Where(x => x.Id >= viewModel.RangeStart && x.Id <= viewModel.RangeEnd)
                .ToListAsync();

            wordList.Words.AddRange(await _appDbContext.Words.Where(x => wordIds.Contains(x.Id)).ToListAsync());

            await _appDbContext.SaveChangesAsync();
        }
    }
}
