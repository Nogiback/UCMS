using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UCMS.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        // Runs when an unhandled exception occurs
        public void OnException(ExceptionContext context)
        {
            // Log the exception details
            _logger.LogError(
                "=== EXCEPTION CAUGHT ===\n" +
                "Exception Type: {ExceptionType}\n" +
                "Message: {Message}\n" +
                "Stack Trace:\n{StackTrace}\n" +
                "Controller: {Controller}\n" +
                "Action: {Action}\n" +
                "========================",
                context.Exception.GetType().Name,
                context.Exception.Message,
                context.Exception.StackTrace,
                context.RouteData.Values["controller"],
                context.RouteData.Values["action"]
            );

            // Console log
            Console.WriteLine($"\n[EXCEPTION FILTER] ERROR in {context.RouteData.Values["controller"]}/{context.RouteData.Values["action"]}");
            Console.WriteLine($"Exception: {context.Exception.Message}\n");

            // Create user-friendly error message
            var userMessage = "Something went wrong while processing your request. Please try again later.";

            // Customize message for specific exceptions (e.g., when trying to delete a course with enrolled students)
            if (context.Exception is InvalidOperationException && 
                context.Exception.Message.Contains("Cannot delete course"))
            {
                userMessage = context.Exception.Message;
            }

            // Use TempData to pass the error message to the redirected error page
            var tempDataProvider = context.HttpContext.RequestServices
                .GetRequiredService<ITempDataProvider>();
            var tempDataDictionaryFactory = context.HttpContext.RequestServices
                .GetRequiredService<ITempDataDictionaryFactory>();
            var tempData = tempDataDictionaryFactory.GetTempData(context.HttpContext);

            // Store error message in TempData
            tempData["ErrorMessage"] = userMessage;
            tempData.Save();

            // Redirect to error page
            context.Result = new RedirectToActionResult("Error", "Home", null);

            // Mark the exception as handled to prevent further propagation
            context.ExceptionHandled = true;
        }
    }
}