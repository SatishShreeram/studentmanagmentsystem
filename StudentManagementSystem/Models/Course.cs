using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Course Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }

        [Range(1, 6)]
        [Display(Name = "Credits")]
        public int Credits { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        // Navigation property
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
