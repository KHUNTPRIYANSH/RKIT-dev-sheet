using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ResultFilters
{
    public class MyResultFilter : IResultFilter
    {
        // normal result filter inheried from IResultFilter only wraps action methods not authentication / resource / exception
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Result filter - [MyResultFilter] after result method execution");

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Result filter - [MyResultFilter] before result method execution");

        }
        // same here also we can create filter with attribute [sync , async]
    }
}
