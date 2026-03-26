using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace UCMS.Filters
{
    public class ActivityLogFilter : IActionFilter
    {
        private readonly ILogger<ActivityLogFilter> _logger;

        public ActivityLogFilter(ILogger<ActivityLogFilter> logger)
        {
            _logger = logger;
        }

        // Runs BEFORE the action executes
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Get user info (or "Guest" if not logged in)
            var username = context.HttpContext.User.Identity?.Name ?? "Guest";

            // Get controller and action names
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            // Get current timestamp
            var timestamp = DateTime.Now;

            // Store start time in HttpContext.Items to calculate duration later
            context.HttpContext.Items["ActionStartTime"] = Stopwatch.GetTimestamp();

            // Log the activity
            _logger.LogInformation(
                "=== ACTION STARTING ===\n" +
                "User: {Username}\n" +
                "Controller: {Controller}\n" +
                "Action: {Action}\n" +
                "Start Time: {Timestamp}\n" +
                "======================",
                username, controllerName, actionName, timestamp
            );

            // Console log
            Console.WriteLine($"\n[ACTION FILTER - BEFORE] User: {username} | {controllerName}/{actionName} | Started: {timestamp:HH:mm:ss}");
        }

        // Runs AFTER the action executes
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Retrieve the start time
            var startTime = (long)context.HttpContext.Items["ActionStartTime"]!;
            var endTime = Stopwatch.GetTimestamp();

            // Calculate duration in milliseconds
            var elapsedMilliseconds = (endTime - startTime) * 1000.0 / Stopwatch.Frequency;

            // Get controller and action names
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            // Check if action executed successfully
            var executedSuccessfully = context.Exception == null;
            var status = executedSuccessfully ? "SUCCESS" : "FAILED";

            // Log the completion of the action
            _logger.LogInformation(
                "=== ACTION COMPLETED ===\n" +
                "Controller: {Controller}\n" +
                "Action: {Action}\n" +
                "Duration: {Duration}ms\n" +
                "Status: {Status}\n" +
                "========================",
                controllerName, actionName, elapsedMilliseconds, status
            );

            // Console log
            Console.WriteLine($"[ACTION FILTER - AFTER] {controllerName}/{actionName} | Duration: {elapsedMilliseconds:F2}ms | Status: {status}\n");
        }
    }
}