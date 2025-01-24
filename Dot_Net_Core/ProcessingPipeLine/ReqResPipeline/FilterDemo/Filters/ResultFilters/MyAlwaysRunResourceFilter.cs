using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ResultFilters
{
    public class MyAlwaysRunResourceFilter : IAlwaysRunResultFilter
    {
        // this filter will run for all other filters though its result filter
        // here also we have sync and async options
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Always Run Result filter - [MyAlwaysRunResourceFilter] after result method execution");

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Always Run Result filter - [MyAlwaysRunResourceFilter] Before result method execution");

        }
    }
}
