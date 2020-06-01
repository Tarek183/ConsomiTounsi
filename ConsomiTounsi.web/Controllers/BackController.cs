using ConsomiTounsi.service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class BackController : Controller
    {
        IUserService US;
        IContactService ContServ;
        public BackController()
        {
            US = new UserService();
            ContServ = new ContactService();
        }
        // GET: Back
        public ActionResult Index()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.authenticated = val1;
            return View(US.GetMany());
        }
        // GET: Back
        public ActionResult IndexContact()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.authenticated = val1;
            return View(ContServ.GetMany());
        }
        // GET: Back/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Back/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Back/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Back/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Back/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Back/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Back/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
