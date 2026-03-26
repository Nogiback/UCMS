using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UCMS.Filters
{
    public class FooterFilter : IResultFilter
    {
        private readonly ILogger<FooterFilter> _logger;

        public FooterFilter(ILogger<FooterFilter> logger)
        {
            _logger = logger;
        }

        // Runs BEFORE the result (view) is rendered
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // Only add footer to ViewResult
            if (context.Result is ViewResult viewResult)
            {
                // Add footer message to ViewData
                viewResult.ViewData["FooterMessage"] = "Conestoga College - UCMS © 2025 – Confidential Academic System";

                var actionName = context.RouteData.Values["action"];
                var controllerName = context.RouteData.Values["controller"];

                // Log the footer addition
                _logger.LogInformation(
                    "=== RESULT EXECUTING ===\n" +
                    "View about to render: {Controller}/{Action}\n" +
                    "Footer message added to ViewData\n" +
                    "========================",
                    controllerName, actionName
                );

                // Console log
                Console.WriteLine($"[RESULT FILTER - BEFORE] Rendering view: {controllerName}/{actionName} | Footer added");
            }
        }

        // Runs AFTER the result (view) is rendered
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ViewResult)
            {
                var actionName = context.RouteData.Values["action"];
                var controllerName = context.RouteData.Values["controller"];
                var timestamp = DateTime.Now;

                // Log the completion of view rendering
                _logger.LogInformation(
                    "=== RESULT EXECUTED ===\n" +
                    "View finished rendering: {Controller}/{Action}\n" +
                    "Completion Time: {Timestamp}\n" +
                    "=======================",
                    controllerName, actionName, timestamp
                );

                // Console log
                Console.WriteLine($"[RESULT FILTER - AFTER] View rendered: {controllerName}/{actionName} | Completed: {timestamp:HH:mm:ss}\n");
            }
        }
    }
}