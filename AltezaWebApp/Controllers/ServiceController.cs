using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AltezaWebApp.Common;
using AltezaWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AltezaWebApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IConfiguration _configuration;
        IEnumerable<MenuModel> _menuModellist;
        public ServiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("our-services")]
        public IActionResult Index()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Email-Campaign-Management-n-Production")]
        public IActionResult EmailCampaign()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Search-Marketing-n-Analytics")]
        public IActionResult SEO()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Content-n-Social-Media-Marketing")]
        public IActionResult SocailMediaMarketing()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Websites-Development")]
        public IActionResult WebsiteDevelopment()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Mobile-n-Web-Apps")]
        public IActionResult MobileAndWebApp()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Software-Solutions")]
        public IActionResult SoftwareSolution()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Perks-n-Benifits")]
        public IActionResult PerksAndBenefits()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Rewards-n-Recognitions")]
        public IActionResult RewardsAndRecognition()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Training-n-Career-Devlopment")]
        public IActionResult Training()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Career")]
        public IActionResult Career()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }

        [Route("Bulk-SMS")]
        public IActionResult BulkSMS()
        {
            //_menuModellist = CommonFunction.GetMenuListIfNull<MenuModel>(_configuration, TempData);
            //ViewBag.WebMenuItems = _menuModellist;
            return View();
        }
        
        [HttpPost]
        public IActionResult SendMailWithAttachment(IList<IFormFile> files, string Name, string Email, string ContactNo, string Position, string Message)
        {
            try
            {
                Name = string.IsNullOrEmpty(Name) ? "" : Name;
                Email = string.IsNullOrEmpty(Email) ? "" : Email;
                ContactNo = string.IsNullOrEmpty(ContactNo) ? "" : ContactNo;
                Position = string.IsNullOrEmpty(Position) ? "" : Position;
                Message = string.IsNullOrEmpty(Message) ? "" : Message;

                var file = files[0];
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtpout.secureserver.net";
                smtp.Port = 80;
                smtp.EnableSsl = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("info@altezatel.com", "alteza@1292");

                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("info@altezatel.com");
                mail.To.Add(new MailAddress("info@altezatel.com"));
                if(file.Length > 0)
                    mail.Attachments.Add(new Attachment(file.OpenReadStream(), Path.GetFileName(file.FileName)));
                mail.IsBodyHtml = true;
                mail.Subject = "Resume sent for " + Position + " Position";

                string message = "<table><tbody>"
                                + "<tr><td><strong>Name :</strong></td><td>"+Name+"</td></tr>"
                                + "<tr><td><strong>Email :</strong></td><td>" + Email + "</td></tr>"
                                + "<tr><td><strong>ContactNo :</strong></td><td>" + ContactNo + "</td></tr>"
                                + "<tr><td><strong>Position :</strong></td><td>" + Position + "</td></tr>"
                                + "<tr><td><strong>Message :</strong></td><td>" + Message + "</td></tr>"
                                + "</table></tbody>";

                mail.Body = message;
                smtp.Send(mail);
            }
            catch (Exception Ex)
            {
                return Json("Error: " + Ex.Message);
            }
            return Json("Success");
        }

        [HttpPost]
        public IActionResult SendEnquiryMail(EnqiryForm enqiryForm)
        {
            try
            {
                string Name = string.IsNullOrEmpty(enqiryForm.Name) ? "" : enqiryForm.Name;
                string Email = string.IsNullOrEmpty(enqiryForm.Email) ? "" : enqiryForm.Email;
                string ContactNo = string.IsNullOrEmpty(enqiryForm.ContactNo) ? "" : enqiryForm.ContactNo;
                string ServiceType = string.IsNullOrEmpty(enqiryForm.ServiceType) ? "" : enqiryForm.ServiceType;
                string Message = string.IsNullOrEmpty(enqiryForm.Message) ? "" : enqiryForm.Message;

                if (!string.IsNullOrEmpty(Name))
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtpout.secureserver.net";
                    smtp.Port = 80;
                    smtp.EnableSsl = false;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("support@altezatel.com", "alteza@1292");

                    MailMessage mail = new MailMessage();
                    mail.From = new System.Net.Mail.MailAddress("support@altezatel.com");
                    mail.To.Add(new MailAddress("support@altezatel.com"));
                    mail.IsBodyHtml = true;
                    mail.Subject = "Enquiry about " + ServiceType;

                    string message = "<table><tbody>"
                                    + "<tr><td><strong>Name :</strong></td><td>" + Name + "</td></tr>"
                                    + "<tr><td><strong>Email :</strong></td><td>" + Email + "</td></tr>"
                                    + "<tr><td><strong>ContactNo :</strong></td><td>" + ContactNo + "</td></tr>"
                                    + "<tr><td><strong>ServiceType :</strong></td><td>" + ServiceType + "</td></tr>"
                                    + "<tr><td><strong>Message :</strong></td><td>" + Message + "</td></tr>"
                                    + "</table></tbody>";

                    mail.Body = message;
                    smtp.Send(mail);
                }
            }
            catch (Exception Ex)
            {
                return Json("Error: " + Ex.Message);
            }
            return Json("Success");
        }

    }
}