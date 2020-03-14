using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AltezaWebApp.Filters
{
    //public class LayoutNameFilter : IActionFilter
    //{
    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        //var result = context.Result as ViewResult;
    //        //var controllerName = context.RouteData.Values["controller"].ToString();
    //        //switch (controllerName)
    //        //{
    //        //    case "ControlPanel":
    //        //        result.ViewData.Add("LayoutName","~/Views/Shared/_CPLayout.cshtml");
    //        //        break;
    //        //    default:
    //        //        result.ViewData.Add("LayoutName","~/Views/Shared/_Layout.cshtml");
    //        //        break;
    //        //}
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //    }
    //}

    //public class AuthenticateUser : TypeFilterAttribute
    //{
    //    public AuthenticateUser(params string[] claim) : base(typeof(CheckUserLoginImpls))
    //    {
    //        Arguments = new object[] { claim };
    //    }
    //}

    //public class CheckUserLoginImpls : IAuthorizationFilter
    //{
    //    readonly string[] _claim;

    //    public CheckUserLoginImpls(params string[] claim)
    //    {
    //        _claim = claim;
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var IsAuthenticated =
    //               context.HttpContext.User.Identity.IsAuthenticated;
    //        var claimsIndentity =
    //               context.HttpContext.User.Identity as ClaimsIdentity;
    //        string ControllerName = Convert.ToString(context.RouteData.Values["Controller"]);
    //        string ActionName = Convert.ToString(context.RouteData.Values["Action"]);

    //        if (ControllerName == "ControlPanel")
    //        {


    //            if (IsAuthenticated)
    //            {
    //                bool flagClaim = false;
    //                foreach (var item in _claim)
    //                {
    //                    if (context.HttpContext.User.HasClaim(item, item))
    //                        flagClaim = true;
    //                }
    //                if (!flagClaim)
    //                {
    //                    //if (context.HttpContext.Request.IsAjaxRequest())
    //                    //    context.HttpContext.Response.StatusCode =
    //                    //    (int)HttpStatusCode.Unauthorized; //Set HTTP 401 
    //                    //                                      //Unauthorized - JRozario
    //                    //else
    //                    if (!context.HttpContext.Request.QueryString.Value.Contains("returnurl"))
    //                    {
    //                        context.Result = new RedirectToRouteResult(new RouteValueDictionary
    //                        {
    //                            { "controller", ControllerName },
    //                            { "action", ActionName }
    //                        });
    //                    }
    //                    //context.Result = new RedirectResult("~/ControlPanel/Login");
    //                }
    //            }
    //            else
    //            {
    //                //if (context.HttpContext.Request.IsAjaxRequest())
    //                //{
    //                //    context.HttpContext.Response.StatusCode =
    //                //      (int)HttpStatusCode.Forbidden; //Set HTTP 403 - JRozario
    //                //}
    //                //else
    //                //{
    //                //if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    //                //{
    //                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
    //                    {
    //                        { "controller", "ControlPanel" },
    //                        { "action", "Login" },
    //                        {"returnurl",context.HttpContext.Request.Path.Value.TrimStart('/') }
    //                    });
    //                //}
    //                //context.Result = new RedirectResult("~/ControlPanel/Login");
    //                //}
    //            }
    //        }
    //        return;
    //    }


    //}
}
