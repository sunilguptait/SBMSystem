using BMS.ViewModels;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace BMS.API.Filters
{
    public class BMSExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<string>()
            {
                ErrorMessage = actionExecutedContext.Exception.Message
            });
        }
    }
}