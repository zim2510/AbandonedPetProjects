using LexicalRes.Models.DTOs;
using LexicalRes.Models.ViewModels;
using LexicalRes.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LexicalRes.Controllers
{
    public class TableManagementController : Controller
    {
        private readonly TableManagementService _tableManagementService;

        public TableManagementController(TableManagementService tableManagementService)
        {
            _tableManagementService = tableManagementService;
        }

        public async Task<ActionResult> Words()
        {
            try
            {
                ViewBag.WordLists = await _tableManagementService.GetWordListDTos();
                var words = await _tableManagementService.GetWordGridModels();
                return View(words);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> InsertWord([FromBody] CrudModelDTO<WordGridModel> data)
        {
            return NotFound();
        }

        public async Task<IActionResult> UpdateWord([FromBody] CrudModelDTO<WordGridModel> data)
        {
            try
            {
                await _tableManagementService.UpdateWord(data.Value);
                return Json(data.Value);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteWord([FromBody] CrudModelDTO<WordGridModel> data)
        {
            return NotFound();
        }

        public async Task<ActionResult> WordLists()
        {
            try
            {
                var wordLists = await _tableManagementService.GetWordListGridModels();
                return View(wordLists);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> InsertWord([FromBody] CrudModelDTO<WordListGridModel> data)
        {
            return NotFound();
        }

        public async Task<IActionResult> UpdateWordList([FromBody] CrudModelDTO<WordListGridModel> data)
        {
            try
            {
                await _tableManagementService.UpdateWordList(data.Value);
                return Json(data.Value);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteWordList([FromBody] CrudModelDTO<WordListGridModel> data)
        {
            return NotFound();
        }
    }
}

