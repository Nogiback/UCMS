using System.ComponentModel.DataAnnotations;

namespace UCMS.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        public string StudentNumber { get; set; } = string.Empty;
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string Program { get; set; } = string.Empty;
        
        // Navigation property
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}