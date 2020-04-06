using BMS.API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    [BMSAuthenticationFilterApi]
    [BMSExceptionFilter]
    public class BaseApiController : ApiController
    {
    }
}
