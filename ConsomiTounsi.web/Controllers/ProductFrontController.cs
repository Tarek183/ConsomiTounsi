using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ConsomiTounsi.Service;
using ConsomiTounsi.web.Models;
using ConsomiTounsi.domain.Entities;

namespace Solution.Web.Controllers {
    public class ProductFrontController : Controller {
        IProductService Service;
        IRayonService ServiceRayon;

        public ProductFrontController()
        {
            Service = new ProductService();
            ServiceRayon = new RayonService();
           /* Product p = new Product
            {
                imageP = "  ",
                barcode = " ",           
                description = "A nice Book ",
                //CategorieP = CategorieP
                dateC = DateTime.Today , 
                dateF = DateTime.Today , 
                marque = "IBM" , 
                price = 12.0 , 
                qte = 100 ,
                CategorieP = Domain.Entities.CategorieP.bananes ,
                Nom = "Game Of Thrones "

            };
            Product pa = new Product
            {
                imageP = "  ",
                barcode = " ",
                description = "A nice Banana  ",
                //CategorieP = CategorieP
                dateC = DateTime.Today,
                dateF = DateTime.Today,
                marque = "Ali ",
                price = 15.0,
                qte = 100,
                CategorieP = Domain.Entities.CategorieP.bananes , 
                Nom = "Bananes" 

            };
            Product pc = new Product
            {
                imageP = "  ",
                barcode = " ",
                description = "A nice Product  ",
                //CategorieP = CategorieP
                dateC = DateTime.Today,
                dateF = DateTime.Today,
                marque = "Dell",
                price = 500.0,
                qte = 100,
                CategorieP = Domain.Entities.CategorieP.bananes ,
                Nom = "Computer Dell "

            };
            Product pd = new Product
            {
                imageP = "  ",
                barcode = " ",
                description = "A nice MacBook Pro  ",
                //CategorieP = CategorieP
                dateC = DateTime.Today,
                dateF = DateTime.Today,
                marque = "Mac ",
                price = 1000.0,
                qte = 100,
                CategorieP = Domain.Entities.CategorieP.bananes , 
                Nom = "MacBook Pro "

            };
            Service.Add(p);
            Service.Add(pa);
            Service.Add(pc);
            Service.Add(pd);
            Service.Commit(); 
            */



        }
        // GET: ProductFront
        public ActionResult Index()
        {
            List<ProductM> Products = new List<ProductM>();
            List<Product> ProductDomain = Service.GetMany().ToList();
            var productss = Service.GetMany();
            foreach (Product p in productss)
            {
                Products.Add(new ProductM
                {
                    IdProduct = p.IdProduct,
                    Nom = p.Nom,
                    imageP = p.imageP,
                    description = p.description,
                    //barcode = 12342,
                    marque = p.marque,
                    price = p.price,
                    dateC = p.dateC,
                    dateF = p.dateF,
                    IdRayon = p.IdRayon


                });

            }
            return View(Products);
        }

        [HttpGet]
        public int GetQuantity(int id)
        {
            return Service.GetById(id).qte;
        }







        // GET: ProductFront/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductFront/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductFront/Create
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

        // GET: ProductFront/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductFront/Edit/5
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

        // GET: ProductFront/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductFront/Delete/5
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
