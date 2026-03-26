using UCMS.Models;

namespace UCMS.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Check if data already exists
            if (context.Courses.Any())
            {
                return;
            }

            // Add courses
            var courses = new List<Course>
            {
                new Course
                {
                    CourseCode = "PROG8555",
                    CourseName = "Microsoft Web Technologies",
                    Description = "ASP.NET Core and C#",
                    Credits = 3,
                    InstructorName = "Professor Haysam Elamin"
                },
                new Course
                {
                    CourseCode = "PROG8111",
                    CourseName = "Mobile Applications Development",
                    Description = "Android and iOS development with React Native",
                    Credits = 3,
                    InstructorName = "Professor Nazih Almalki"
                },
                new Course
                {
                    CourseCode = "INFO8373",
                    CourseName = "Cybersecurity for Software Development",
                    Description = "Security best practices for software development",
                    Credits = 3,
                    InstructorName = "Professor Shanti Couvrette"
                }
            };
            context.Courses.AddRange(courses);

            // Add students
            var students = new List<Student>
            {
                new Student
                {
                    StudentNumber = "9086580",
                    FirstName = "Peter",
                    LastName = "Do",
                    Email = " pdo6580@conestogac.on.ca",
                    Program = "Computer Applications Development"
                },
                new Student
                {
                    StudentNumber = "100234567",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jsmith4567@conestogac.on.ca",
                    Program = "Computer Engineering"
                },
                 new Student
                {
                    StudentNumber = "122987433",
                    FirstName = "Bob",
                    LastName = "Reynolds",
                    Email = "breynolds7433@conestogac.on.ca",
                    Program = "Software Engineering Technology"
                }
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            // Add student enrollments
            var enrollments = new List<StudentCourse>
            {
                new StudentCourse
                {
                    StudentId = students[0].Id,
                    CourseId = courses[0].Id, // Enroll Peter in Microsoft Web Technologies
                    EnrolledDate = DateTime.Now
                },
                new StudentCourse
                {
                    StudentId = students[1].Id,
                    CourseId = courses[0].Id,  // Enroll Jane in Microsoft Web Technologies
                    EnrolledDate = DateTime.Now
                }
            };
            context.StudentCourses.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}   
