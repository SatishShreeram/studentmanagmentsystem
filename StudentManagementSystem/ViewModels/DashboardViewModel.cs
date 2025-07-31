using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public List<Student> RecentStudents { get; set; }
        public List<CourseEnrollmentViewModel> PopularCourses { get; set; }
    }
}
