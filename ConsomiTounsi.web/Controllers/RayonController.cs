using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Collections;
using ConsomiTounsi.Service;
using ConsomiTounsi.web.Models;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using Microsoft.AspNet.Identity;

namespace Solution.Web.Controllers {
    public class RayonController : Controller {
        IRayonService Service = null;
        IProductService ServiceProduct;
        IUserService US;
        public RayonController()
        {
            Service = new RayonService();
            ServiceProduct = new ProductService();
            US = new UserService();

            //rayonService = new rayonService();
        }

        // GET: Rayon
        public ActionResult Index()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            var user = US.GetById(userId);
            string UserName = user.UserName;
            string Phone = user.Phone;
            string mail = user.Email;
            string image = user.image;
            string role = user.Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;

            ViewBag.Role = role;
            ViewBag.authenticated = val1;

            List<RayonM> Rayons = new List<RayonM>();
            foreach (Rayon r in Service.GetMany())
            {
                Rayons.Add(new RayonM
                {
                    IdRayon = r.IdRayon,
                    imageR = r.imageR,
                    typeR = r.typeR,
                    description = r.description,
                    flag = r.flag
                }); ;

            }
            return View(Rayons);
        }
        public ActionResult IndexClient()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            var user = US.GetById(userId);
            string UserName = user.UserName;
            string Phone = user.Phone;
            string mail = user.Email;
            string image = user.image;
            string role = user.Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;

            ViewBag.Role = role;
            ViewBag.authenticated = val1;   
            List<RayonM> Rayons = new List<RayonM>();
            foreach (Rayon r in Service.GetMany())
            {
                Rayons.Add(new RayonM
                {
                    IdRayon = r.IdRayon,
                    imageR = r.imageR,
                    typeR = r.typeR,
                    description = r.description,
                    flag = r.flag
                }); ;

            }
            return View(Rayons);
        }

        // GET: Rayon/Details/5
        public ActionResult Details(int? id)
        {

            Rayon rayon = new Rayon();
            RayonM r = new RayonM();
            rayon = Service.GetById((int)id);
            int idr = rayon.IdRayon;
            r.IdRayon = rayon.IdRayon;
            r.imageR = rayon.imageR;
            r.typeR = rayon.typeR;
            r.description = rayon.description;
            r.flag = rayon.flag;
            //var productss = ServiceProduct.GetMany();
            //productss = productss.Where(p => p.IdRayon == idr);
            //r.products = productss;
            return View(r);
        }
        public ActionResult DetailsClient(int? id)
        {

            Rayon rayon = new Rayon();
            RayonM r = new RayonM();
            rayon = Service.GetById((int)id);
            r.IdRayon = rayon.IdRayon;
            r.imageR = rayon.imageR;
            r.typeR = rayon.typeR;
            r.description = rayon.description;
            r.flag = rayon.flag;
            //r.products = rayon.products;
           List<Product> Products = ServiceProduct.GetMany().ToList();
            Products = Products.Where(p => p.IdRayon == id).ToList();
            //Products = Products.GroupBy(p => p.CategorieP);
            ViewBag.MyProducts = new SelectList(Products, "IdProduct", "CategorieP");
            return View(r);
        }

        // GET: Rayon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rayon/Create
        [HttpPost]
        public ActionResult Create(RayonM RayonM, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid || Image == null || Image.ContentLength == 0)
            {
                RedirectToAction("Create");
            }
            Rayon r = new Rayon()
            {
                IdRayon = RayonM.IdRayon,
                imageR = Image.FileName,
                //imageR = RayonM.imageR,
                typeR = RayonM.typeR,
                description = RayonM.description,
                flag = RayonM.flag


            };

            Service.Add(r);
            Service.Commit();

            var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), Image.FileName);
            Image.SaveAs(path);

            return (RedirectToAction("Index"));
        }

        // GET: Rayon/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rayon/Edit/5
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

        // GET: Rayon/Delete/5
        public ActionResult Delete(int id)
        {
            Rayon rayon = new Rayon();
            RayonM r = new RayonM();
            rayon = Service.GetById((int)id);
            r.IdRayon = rayon.IdRayon;
            r.imageR = rayon.imageR;
            r.typeR = rayon.typeR;
            r.description = rayon.description;
            r.flag = rayon.flag;
            return View(r);
        }

        // POST: Rayon/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Rayon rayon = Service.GetById((int)id);
            Service.Delete(rayon);
            Service.Commit();
            return RedirectToAction("Index");
        }




        // GET: Rayon/Categories/categorie
        public ActionResult Categories(string categorie)
        {

            Product product = new Product();
            List<Product> Products = ServiceProduct.GetMany().ToList();
            //for (int i = 0; i < Products.Count(); i++) 
            ///{
             // Products = Products.Where(pr => pr.CategorieP.ToString() == categorie).ToList();

            //}
            Products = Products.Where(pr => pr.CategorieP.ToString() == "Desserts").ToList();
            //Products = Products.Where(pr => pr.CategorieP.ToString() == categorie).ToList();

            ViewBag.MyProducts = new SelectList(Products, "IdProduct", "Nom");
            return View();
        }
      

    }
}
