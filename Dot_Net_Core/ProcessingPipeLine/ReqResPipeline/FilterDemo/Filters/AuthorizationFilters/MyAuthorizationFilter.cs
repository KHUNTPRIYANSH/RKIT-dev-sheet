using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters.AuthorizationFilters
{
    public class MyAuthorizationFilter : IAuthorizationFilter
    {
        // here unlike resource , action filter we only have on method no after before as we have only one job to handle authorization
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine($"Authorization filter - [MyAuthorizationFilter] context : {context}");
        }
    }
}
