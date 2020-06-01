using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class HomeController : Controller
    {
        IContactService ContactService;
        IUserService US;
        public HomeController()
        {
            ContactService = new ContactService();
            US = new UserService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            string image = US.GetById(userId).image;
            string role = US.GetById(userId).Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.image = image;
            ViewBag.Role = role;

            ViewBag.authenticated = val1;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult UpdateAbout()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            string image = US.GetById(userId).image;
            string role = US.GetById(userId).Role;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.image = image;
            ViewBag.authenticated = val1;
            ViewBag.Role = role;

            var model = new EditViewModel
            {
                Email = US.GetById(userId).Email,
                UserName = US.GetById(userId).UserName,
                Phone = US.GetById(userId).Phone,
                Image = US.GetById(userId).image,
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAbout(EditViewModel model, HttpPostedFileBase file)
        {
            try
            {
                var userId = User.Identity.GetUserId();

            var thisUser = US.GetById(userId);
            thisUser.UserName = model.UserName;
            thisUser.Email = model.Email;
            thisUser.Phone = model.Phone;
            thisUser.image = file.FileName;
            var fileName = "";

            if (file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"),fileName);
                file.SaveAs(path);
            }
            US.Update(thisUser);
            US.Commit();

            var verifyurl = "/Signup/VerifiyAccount/";
            var link = Request.Url.AbsolutePath.Replace(Request.Url.PathAndQuery, verifyurl);

            var fromEmail = new MailAddress("tarek.bettaieb@esprit.tn", "Bettaieb Tarek");
            var toEmail = new MailAddress("bettaiebtarek10@gmail.com");
            var FromEmailPassword = "183JMT1268";

            string subject = "Account updated";

            string body = "Dear Client we have just received an account update request for bettaiebtarek10@gmail.com" + "<br/>" + "If you would like to view your account details, you can login at the following link: https://localhost:44382/Account/Login " +
                "<br/><a href = '" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, FromEmailPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            }) smtp.Send(message);
            
            }
            catch
            {
                return RedirectToAction("About", "Home");
            }
            return RedirectToAction("About", "Home");
        }

        public ActionResult Contact()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            string image = US.GetById(userId).image;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.image = image;
            ViewBag.authenticated = val1;
            ViewBag.Message = "Your contact page.";
            return View();
        }
        // GET: Post/Create
        public ActionResult CreateContact()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            string image = US.GetById(userId).image;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.image = image;
            ViewBag.authenticated = val1;
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost]
        public ActionResult CreateContact(ContactModel cm)
        {
            Contact c = new Contact();

            c.ContactContent = cm.ContactContent;
            c.UserName = cm.UserName;
            c.UserEmail = cm.UserEmail;
            c.UserPhone = cm.UserPhone;
            c.ContactDate = DateTime.Now;

            c.UserId = User.Identity.GetUserId<string>();
            ContactService.Add(c);
            ContactService.Commit();
            return RedirectToAction("Index", "Post");
        }
    }
}