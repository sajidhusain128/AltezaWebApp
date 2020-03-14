using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AltezaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using AltezaWebApp.BAL;
using AltezaWebApp.Common;

namespace AltezaWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("")]
        //[Route("Home")]
        public IActionResult Index()
        {
            //IEnumerable<MenuModel> menuModelList = null;
            try
            {
                //menuModelList = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration,TempData);
                //ViewBag.WebMenuItems = menuModelList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return View();
        }

        [Route("AboutUs")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            //IEnumerable<MenuModel> menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = menuModellist;
            return View();
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            ViewData["Message"] = "Your contact page.";
            //IEnumerable<MenuModel> menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = menuModellist;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
