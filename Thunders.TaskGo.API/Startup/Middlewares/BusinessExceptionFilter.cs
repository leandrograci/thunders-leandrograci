using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Thunders.TaskGo.Domain.Exceptions;

namespace Thunders.TaskGo.Web.Startup.Middlewares
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException businessException)
            {
                context.Result = new ObjectResult(businessException.Message)
                {
                    StatusCode = 400 
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
