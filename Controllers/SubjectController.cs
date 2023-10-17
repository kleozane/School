using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SchoolContext _context;

        public SubjectController(SchoolContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            if (_context.Subjects != null)
            {
                return View(await _context.Subjects.Include(x => x.Curriculum).ToListAsync());
            }
            else
            {
                return Problem("Nuk ka asnje lende!");
            }
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subjects == null || id == 0)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.Include(x => x.Curriculum).FirstOrDefaultAsync(m => m.Id == id);
            return View(subject);
        }


        public IActionResult Create()
        {
            var curriculums = _context.Curriculums.ToList();
            ViewBag.Curriculums = curriculums;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,Type,CurriculumId")] Subject subject)
        {
            _context.Add(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subjects == null || id == 0)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,Type")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(subject);
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
            if (_context.Subjects == null)
            {
                return Problem("Nuk ka lende!");
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
            

            return RedirectToAction(nameof(Index));
        }
    }
}
