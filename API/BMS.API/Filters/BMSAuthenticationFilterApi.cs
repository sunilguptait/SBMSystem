using BMS.API.JWT;
using BMS.ViewModels;
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
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BMS.API.Filters
{
    public class BMSAuthenticationFilterApi : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (SkipAuthorization(filterContext))
            {
                return;
            }

            if (!IsUserAuthorized(filterContext))
            {
                ShowAuthenticationError(filterContext);
                return;
            }
            base.OnAuthorization(filterContext);
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        public bool IsUserAuthorized(HttpActionContext actionContext)
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

        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            string action = filterContext.ActionDescriptor.ActionName ?? "";
            if (action.ToLower() == "getnewtoken")
            {
                var responseDTO = new ResponseModel<string>() { ErrorMessage = "UnableToGetToken" };
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, responseDTO);
            }
            else
            {
                var responseDTO = new ResponseModel<string>() { ErrorMessage = "Unable to access, Please login again" };
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, responseDTO);
            }
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }
    }
}