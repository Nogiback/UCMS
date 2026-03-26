using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UCMS.Data;
using UCMS.Filters;

namespace UCMS.Controllers
{
    // Applying the ActivityLogFilter to all actions in this controller
    [ServiceFilter(typeof(ActivityLogFilter))]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.StudentCourses)
                .ToListAsync();
            return View(courses);
        }

        // GET: Course/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.StudentCourses)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // Error check: Prevent deletion if the course has enrolled students
            if (course.StudentCourses.Any())
            {
                throw new InvalidOperationException($"Cannot delete course {course.CourseCode} because it has enrolled students.");
            }

            return View(course);
        }
    }
}