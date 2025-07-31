using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly StudentDbContext _context;

        public EnrollmentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .ToListAsync();
                
            return View(enrollments);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName");
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title");
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,Grade,EnrollmentDate")] Enrollment enrollment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if enrollment already exists
                    var existingEnrollment = await _context.Enrollments
                        .Where(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId)
                        .FirstOrDefaultAsync();

                    if (existingEnrollment != null)
                    {
                        ModelState.AddModelError("", "This student is already enrolled in this course.");
                    }
                    else
                    {
                        _context.Add(enrollment);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", enrollment.CourseId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", enrollment.CourseId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentToUpdate = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == id);
            
            if (await TryUpdateModelAsync<Enrollment>(
                enrollmentToUpdate,
                "",
                e => e.Grade, e => e.EnrollmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", enrollmentToUpdate.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", enrollmentToUpdate.CourseId);
            return View(enrollmentToUpdate);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            
            if (enrollment == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists, see your system administrator.";
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            
            if (enrollment == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
