using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, Title = "Introduction to C#", CourseCode = "CS101", Credits = 3, Department = "Computer Science", Description = "Basic C# programming concepts" },
                new Course { CourseId = 2, Title = "Web Development", CourseCode = "CS102", Credits = 4, Department = "Computer Science", Description = "Web development using ASP.NET Core" },
                new Course { CourseId = 3, Title = "Database Systems", CourseCode = "CS103", Credits = 4, Department = "Computer Science", Description = "Introduction to database design and SQL" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { 
                    StudentId = 1, 
                    FirstName = "John", 
                    LastName = "Doe", 
                    DateOfBirth = new System.DateTime(2000, 1, 1), 
                    Email = "john.doe@example.com", 
                    PhoneNumber = "123-456-7890", 
                    Address = "123 Main St", 
                    EnrollmentDate = new System.DateTime(2025, 1, 15) 
                },
                new Student { 
                    StudentId = 2, 
                    FirstName = "Jane", 
                    LastName = "Smith", 
                    DateOfBirth = new System.DateTime(2001, 5, 10), 
                    Email = "jane.smith@example.com", 
                    PhoneNumber = "987-654-3210", 
                    Address = "456 Oak Ave", 
                    EnrollmentDate = new System.DateTime(2025, 4, 20) 
                }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentId = 1, StudentId = 1, CourseId = 1, Grade = Grade.A, EnrollmentDate = new System.DateTime(2025, 1, 15) },
                new Enrollment { EnrollmentId = 2, StudentId = 1, CourseId = 2, Grade = Grade.B, EnrollmentDate = new System.DateTime(2025, 1, 15) },
                new Enrollment { EnrollmentId = 3, StudentId = 2, CourseId = 1, Grade = Grade.B, EnrollmentDate = new System.DateTime(2025, 4, 20) },
                new Enrollment { EnrollmentId = 4, StudentId = 2, CourseId = 3, Grade = Grade.A, EnrollmentDate = new System.DateTime(2025, 4, 20) }
            );
        }
    }
}
