using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.ExceptionFilters
{
    public class MyExceptionFilter : IExceptionFilter
    {
        // here unlike resource , action filter we only have on method no after before as we have only one job to handle exception
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"Exception filter - [MtExceptionFilter] error : {context}");
        }
    }
}
