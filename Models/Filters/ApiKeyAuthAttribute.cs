using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Global_Intern.Models.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // before
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentailKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apikey = config.GetValue<string>(key: "ApiKey");
            if (!apikey.Equals(potentailKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }



            await next();
        }
    }
}
