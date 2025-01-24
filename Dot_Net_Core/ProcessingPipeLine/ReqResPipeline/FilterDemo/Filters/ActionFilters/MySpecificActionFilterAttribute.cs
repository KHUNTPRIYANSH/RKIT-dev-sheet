using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FilterDemo.Filters.ActionFilters
{
    public class MySpecificActionFilterAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        private readonly string _action;

        public MySpecificActionFilterAttribute(string action, int order = 0)
        {
            _action = action ?? "Unknown Action";
            Order = order;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Action filter - [MySpecificActionFilter] After executing: {_action}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Action filter - [MySpecificActionFilter] Before executing: {_action}");
        }

        public int Order { get; set; }

    }
}
