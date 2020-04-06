using BMS.API.Filters;
using BMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BMS.API.Controllers
{
    [BMSAuthenticationFilter]
    public class BaseController : Controller, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var viewResult = new JsonResult();
                viewResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                viewResult.Data = new ResponseModel<string>() { ErrorMessage = filterContext.Exception.Message };
                filterContext.Result = viewResult;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}