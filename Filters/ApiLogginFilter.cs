using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLogginFilter : IActionFilter
    {
        private readonly ILogger<ApiLogginFilter> _logger;

        public ApiLogginFilter(ILogger<ApiLogginFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Executando: ActionExecuting");
            _logger.LogInformation(DateTime.Now.ToString());
            _logger.LogInformation($"Satatus code: {context.ModelState.IsValid}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Executando: ActionExecuted");
            _logger.LogInformation(DateTime.Now.ToString());
            _logger.LogInformation($"Satatus code: {context.HttpContext.Response.StatusCode}");
        }
    }
}
