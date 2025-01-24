using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ActionFilters
{
    public class MyActionFilterAsyncAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _name;
        public MyActionFilterAsyncAttribute(string name)
        {
            _name = name;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Action filter - [MyActionFilterAsyncAttribute] before : " + _name);
            await next();
            Console.WriteLine("Action filter - [MyActionFilterAsyncAttribute] after : " + _name);
        }
    }
}
