using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;

namespace School.Controllers
{
    public class CurriculumController : Controller
    {
        private readonly SchoolContext _context;

        public CurriculumController(SchoolContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            if (_context.Curriculums != null)
            {
                return View(await _context.Curriculums.ToListAsync());
            }
            else
            {
                return Problem("Nuk ka asnje kurrikul!");
            }
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Curriculums == null || id == 0)
            {
                return NotFound();
            }

            var curriculum = await _context.Curriculums.Include(x => x.Subjects).FirstOrDefaultAsync(m => m.Id == id);
            return View(curriculum);
        }
    }
}
