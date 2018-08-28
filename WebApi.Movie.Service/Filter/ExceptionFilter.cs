namespace WebApi.Movie.Service.Filters
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<ExceptionFilter> _Logger;
        private IHostingEnvironment _env;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, IHostingEnvironment env)
        {
            _Logger = logger;
            _env = env;
        }

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                return;
            }
           
            var ex = actionExecutedContext.Exception;
            actionExecutedContext.HttpContext.Response.StatusCode = 500;

            _Logger.LogError($"Application thrown error: {ex.Message}", ex);

            base.OnException(actionExecutedContext);
        }
    }
}
