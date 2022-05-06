using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FrizzApp.Api.ControllerSecurity
{
    public class CreateKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string CreateKeyHeaderName = "CreateProductKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(CreateKeyHeaderName, out var potentialCreateKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var createProductKey = configuration.GetValue<string>("CreateProductKey");

            if (!createProductKey.Equals(potentialCreateKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
