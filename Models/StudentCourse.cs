namespace UCMS.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledDate { get; set; } = DateTime.Now;
        
        // Navigation properties
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}