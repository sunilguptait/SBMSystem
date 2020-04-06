using BMS.API.JWT;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace BMS.API.Filters
{
    public class BMSAuthenticationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SkipAuthorization(filterContext))
            {
                return;
            }

            if (!IsUserAuthorized(filterContext))
            {
                //var viewResult = new JsonResult();
                //viewResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                //viewResult.Data = new ResponseModel() { Message = "Unable to access, Please login again" };
                filterContext.Result = new HttpStatusCodeResult(401);
                return;
            }
        }

        private static bool SkipAuthorization(AuthorizationContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                   actionContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
        }

        public bool IsUserAuthorized(AuthorizationContext actionContext)
        {
            var authHeader = FetchFromHeader(actionContext); //fetch authorization token from header
            if (authHeader != null)
            {
                var auth = new JWTManager();
                JwtSecurityToken userPayloadToken = auth.GenerateUserClaimFromJWT(authHeader);
                if (userPayloadToken != null)
                {
                    var identity = auth.PopulateUserIdentity(userPayloadToken);
                    string[] roles = { "All" };
                    var genericPrincipal = new GenericPrincipal(identity, roles);
                    Thread.CurrentPrincipal = genericPrincipal;
                    var authenticationIdentity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;
                    if (authenticationIdentity != null && !String.IsNullOrEmpty(authenticationIdentity.UserName))
                    {
                        authenticationIdentity.UserName = identity.UserName;
                    }
                    return true;
                }

            }
            return false;


        }

        private string FetchFromHeader(AuthorizationContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.HttpContext.Request.Headers["Authorization"];
            if (authRequest != null)
            {
                var arr = authRequest.Split(' ');
                if (arr != null && arr.Length > 1)
                {
                    requestToken = arr[1];
                }
            }

            return requestToken;
        }
    }
}