using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

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


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Weeks,Hours")] Curriculum curriculum)
        {
            _context.Add(curriculum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Curriculums == null || id == 0)
            {
                return NotFound();
            }

            var curriculum = await _context.Curriculums.FindAsync(id);
            return View(curriculum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Weeks,Hours")] Curriculum curriculum)
        {
            if (id != curriculum.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(curriculum);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Curriculums == null)
            {
                return Problem("Nuk ka kurrikula!");
            }
            var curriculum = await _context.Curriculums.FindAsync(id);
            if (curriculum != null)
            {
                var subjectsInCurriculum = _context.Subjects.Where(s => s.CurriculumId == curriculum.Id).ToList();

                foreach (var subject in subjectsInCurriculum)
                {
                    subject.CurriculumId = null;
                    _context.Subjects.Update(subject);
                    await _context.SaveChangesAsync();
                }
               
                _context.Curriculums.Remove(curriculum);
                await _context.SaveChangesAsync();
            }

            
            return RedirectToAction(nameof(Index));
        }
    }
}
