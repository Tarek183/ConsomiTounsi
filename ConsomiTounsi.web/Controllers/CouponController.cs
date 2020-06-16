using ConsomiTounsi.service;
using ConsomiTounsi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class CouponController : Controller
    {
        // GET: Coupon

        public ICouponsService couponsService;
        public CouponController()
        {
            couponsService = new CouponsService();
          /* Coupons c = new Coupons 
            { 
           
                date = DateTime.Today ,
                duree = DateTime.Now , 
                etat = 1 , 
                percontage = 12
            };
            Coupons ca = new Coupons
            {

                date = DateTime.Today,
                duree = DateTime.Now,
                etat = 1,
                percontage = 12
            };
            Coupons cb = new Coupons
            {

                date = DateTime.Today,
                duree = DateTime.Now,
                etat = 1,
                percontage = 12
            };
            Coupons cd = new Coupons
            {

                date = DateTime.Today,
                duree = DateTime.Now,
                etat = 1,
                percontage = 12
            };
            couponsService.Add(c);
            couponsService.Add(ca);
            couponsService.Add(cb);
            couponsService.Add(cd);
            couponsService.Commit(); 
            */

        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Coupon/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Coupon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coupon/Create
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

        // GET: Coupon/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Coupon/Edit/5
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

        // GET: Coupon/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Coupon/Delete/5
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
