using Microsoft.AspNetCore.Mvc.Filters;


    public class ActionFilterExample : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
    namespace ActionFilters.Filters
    {
        public class AsyncActionFilterExample : IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
            {
                // execute any code before the action executes
                var result = await next();
                // execute any code after the action executes
            }
        }
    }

