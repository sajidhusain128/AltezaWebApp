using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltezaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using AltezaWebApp.BAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AltezaWebApp.Filters;

namespace AltezaWebApp.Controllers
{
    public class ControlPanelController : Controller
    {
        private readonly IConfiguration _configuration;
        public ControlPanelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("ControlPanel/")]
        [Route("ControlPanel/Index")]
        [Route("ControlPanel/Login")]     
        [HttpGet]
        public IActionResult Index()
        {
            ConnectDB ConnectDB = new ConnectDB(_configuration);
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel loginModel,string returnurl)
        {
            IActionResult response = null;
            if (ModelState.IsValid)
            {
                response = Unauthorized();
                var user = AuthenticateUser(loginModel);
                string RoleName = "";
                if (user != null)
                {
                    if (user.Username.ToLower() == "admin")
                        RoleName = "Admin";
                    else
                        RoleName = "User";
                    var tokenString = GenerateJSONWebToken(user);
                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, RoleName)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    if (loginModel.RememberMe)
                    {
                        props.IsPersistent = loginModel.RememberMe;
                        props.ExpiresUtc = DateTime.Now.AddSeconds(10);
                    }

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                    response = Ok(new { token = tokenString });
                }
                else
                {
                    ViewBag.ErrorMsg = "Invalid Username and Password";
                    ModelState.AddModelError(string.Empty, "Invalid Username and Password.");
                    return View(user);
                }
                    
            }
            if (string.IsNullOrEmpty(returnurl))
                return RedirectToAction("Dashboard");
            else
                return RedirectToAction(returnurl);
        }

        private string GenerateJSONWebToken(LoginModel loginModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private LoginModel AuthenticateUser(LoginModel login)
        {
            LoginModel loginModel = null;
            UserBAL userBAL = new UserBAL();
            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            loginModel = userBAL.GetValidateUser(_configuration, login);

            if (loginModel != null)
            {
                if (login.Username == loginModel.Username && login.Password == loginModel.Password)
                {
                    loginModel = new LoginModel { Username = loginModel.Username, Password = loginModel.Password };
                }
            }
            return loginModel;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}