using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StudentDbContext _context;

    public HomeController(ILogger<HomeController> logger, StudentDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Create a dashboard view model with summary data
        var dashboardViewModel = new DashboardViewModel
        {
            TotalStudents = await _context.Students.CountAsync(),
            TotalCourses = await _context.Courses.CountAsync(),
            TotalEnrollments = await _context.Enrollments.CountAsync(),
            RecentStudents = await _context.Students
                .OrderByDescending(s => s.EnrollmentDate)
                .Take(5)
                .ToListAsync(),
            PopularCourses = await _context.Courses
                .Select(c => new CourseEnrollmentViewModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    CourseCode = c.CourseCode,
                    EnrollmentCount = c.Enrollments.Count
                })
                .OrderByDescending(c => c.EnrollmentCount)
                .Take(5)
                .ToListAsync()
        };

        return View(dashboardViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
