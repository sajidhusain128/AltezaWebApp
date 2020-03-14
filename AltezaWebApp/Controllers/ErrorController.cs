using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AltezaWebApp.Controllers
{
    public class ErrorController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        private readonly IConfiguration _configuration;
        public ErrorController(IConfiguration configuration, TelemetryClient telemetryClient)
        {
            _configuration = configuration;
            _telemetryClient = telemetryClient;
        }
        public IActionResult Index(int? code)
        {
            //var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            string errorTitle = "";
            string errorMsg = "";
            if (code == 404)
            {
                errorTitle = "Page Not Found";
                errorMsg = "";
            }
            else if (code == 505)
            {
                errorTitle = "Internal Server Error";
                errorMsg = "";
            }
            else if (code == 401)
            {
                errorTitle = "Unauthorized access";
                errorMsg = "";
            }
            ViewBag.ErrorCode = code;
            ViewBag.ErrorTitle = errorTitle;
            ViewBag.ErrorMsg = errorMsg;
            return View();
        }

        [Route("InternalServerError")]
        public IActionResult InternalServerError()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _telemetryClient.TrackException(exceptionHandlerPathFeature.Error);
            _telemetryClient.TrackEvent("Error.ServerError", new Dictionary<string, string>
            {
                ["originalPath"] = exceptionHandlerPathFeature.Path,
                ["error"] = exceptionHandlerPathFeature.Error.Message
            });
            ViewBag.Error = exceptionHandlerPathFeature.Error.Message;
            return View();
        }

        [Route("PageNotFound")]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            _telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            {
                ["originalPath"] = originalPath
            });
            return View();
        }
    }
}