using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ResourceFilters
{
    public class MyResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Resource filter - [MyResourceFilter] after resource method execution");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Resource filter - [MyResourceFilter] before resource method execution");

        }
    }
    // same here also we can create filter with attribute [sync , async]
}
