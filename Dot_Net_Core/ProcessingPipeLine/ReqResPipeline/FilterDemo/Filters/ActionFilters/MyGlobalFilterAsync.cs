using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ActionFilters
{
    public class MyGlobalFilterAsync : IAsyncActionFilter
    {
        private readonly string _name;

        public MyGlobalFilterAsync(string name)
        {
            _name = name;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Action filter -  [MyGlobalFilterAsync] before  : " + _name);
            await next();
            Console.WriteLine("Action filter -  [MyGlobalFilterAsync] after  : " + _name);
        }
    }
}
