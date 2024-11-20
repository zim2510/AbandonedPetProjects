using Core.Data;
using Core.Models;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly AppDbContext _context;

        public ProblemsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Problems = await _context.Problems.Include(x => x.ProblemTags).ToArrayAsync();
            return View(Problems);
        }

        public async Task<IActionResult> Insert([FromBody] GridCrudViewModel<Problem> data)
        {
            _context.Problems.Add(data.Value);
            await _context.SaveChangesAsync();
            return Json(data.Value);
        }

        public async Task<IActionResult> Update([FromBody] GridCrudViewModel<Problem> data)
        {
            _context.Problems.Update(data.Value);
            await _context.SaveChangesAsync();
            return Json(data.Value);
        }

        public async Task<IActionResult> Delete([FromBody] GridCrudViewModel<Problem> data)
        {
            int id = Int32.Parse(data.Key.ToString());
            var entity = await _context.Problems.FirstOrDefaultAsync(x => x.ProblemId == id);
            _context.Problems.Remove(entity);
            await _context.SaveChangesAsync();
            return Json(data);
        }
    }
}
