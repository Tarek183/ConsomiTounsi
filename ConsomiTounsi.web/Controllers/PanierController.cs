﻿using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class PanierController : Controller {
        // GET: Panier
        public static Panier pannier { get; set; }
        public PanierController()
        {

        }
        public static Panier Instance()
        {
            if (pannier == null)
            {
                pannier = new Panier
                {
                    date = DateTime.Now,
                    ClientId = 1
                };
            }
            return pannier;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Panier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Panier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panier/Create
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

        // GET: Panier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panier/Edit/5
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

        // GET: Panier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Panier/Delete/5
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