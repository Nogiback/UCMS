using System.ComponentModel.DataAnnotations;

namespace UCMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        
        [Required]
        public string CourseCode { get; set; } = string.Empty;
        
        [Required]
        public string CourseName { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public int Credits { get; set; }
        
        public string InstructorName { get; set; } = string.Empty;
        
        // Navigation property
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}