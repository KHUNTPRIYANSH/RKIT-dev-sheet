using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ActionFilters
{
    public class MyGlobalFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action filter - [MyGlobalfilter] after action method execution");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action filter - [MyGlobalfilter] before action method execution");
        }
    }
}
