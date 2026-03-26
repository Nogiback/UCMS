# University Course Management System (UCMS)
## Assignment 3 - MVC Filters Implementation

### Student Information
- **Name:** Peter Do
- **Student ID:** 9086580
- **Course:** PROG8555 - Microsoft Web Technologies

### Project Description
ASP.NET Core MVC application demonstrating three types of filters:
- Action Filter (ActivityLogFilter)
- Result Filter (FooterFilter)
- Exception Filter (GlobalExceptionFilter)

### How to Run
1. Clone the repository
2. Navigate to the UCMS folder
3. Run `dotnet restore`
4. Run `dotnet ef database update`
5. Run `dotnet run`
6. Open browser to `https://localhost:5xxx`

### Technologies Used
- ASP.NET Core MVC (.NET 10)
- Entity Framework Core
- SQLite Database
- Bootstrap 5

### Filter Implementations

**Action Filter (ActivityLogFilter):**
- Applied to: CourseController
- Logs: User, Controller, Action, Timestamps, Duration

**Result Filter (FooterFilter):**
- Applied: Globally
- Adds footer message to all views

**Exception Filter (GlobalExceptionFilter):**
- Applied: Globally
- Catches and logs all unhandled exceptions
- Redirects to friendly error page
