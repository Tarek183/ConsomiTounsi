using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.Service;
using ConsomiTounsi.web.Models;
using Solution.Service;
using Solution.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class LigneCommandController : Controller {

        public ILigneCommandService ligneCommandService;
        public IProductService serviceProduit;
        public IPanierService panierService;

        public Panier panier;
        public LigneCommandController()
        {
            ligneCommandService = new LigneCommandService();
            panierService = new PanierService();
            serviceProduit = new ProductService();
            panier = PanierController.Instance();


        }

        // GET: LigneCommand
        public ActionResult Index()
        {
            List<LigneCommand> l = new List<LigneCommand>();
            List<LigneCommandM> lm = new List<LigneCommandM>();
            Product pLocal = new Product();
            l = ligneCommandService.GetAllPanier(panier.idPanier);
            foreach (LigneCommand L in l)
            {
                pLocal = serviceProduit.HetProduct(L.ProduitId);
                lm.Add(new LigneCommandM
                {
                    date = L.date,
                    quantite = L.quantite,
                    idLigneCommand = L.idLigneCommand,
                    produit = new ProductM
                    {
                        price = pLocal.price,
                        Nom = pLocal.Nom,
                        marque = pLocal.marque
                    }
                });

            }




            return View(  lm );
        }

        // GET: LigneCommand/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ProductM GetByidProduct(int id)
        {

            Product product = serviceProduit.GetById(id);
            ProductM productm = new ProductM
            {
                IdProduct = product.IdProduct
            };

            return productm;
        }
        public int createLigneCommand(int id)
        {
            Product p = new Product
            {
                description = "dasf",
                Nom = "ads",
                dateC = DateTime.Today,
                dateF = DateTime.Today
            };
            LigneCommand ligneCommand = new LigneCommand
            {
                date = DateTime.Today,
                produit = p,
                quantite = 1,
                panier = panier
            };

            panierService.Add(panier);
            panierService.Commit();
            serviceProduit.Add(p);
            serviceProduit.Commit();
            ligneCommandService.Add(ligneCommand);
            ligneCommandService.Commit();



            return 0;
        }
        [HttpGet]
        public void CreateCommandLineApi(int id)
        {
            Product p = serviceProduit.HetProduct(id);
            if (ligneCommandService.exist(panier.idPanier, p.IdProduct) == 0)
            {

                LigneCommand ligneCommand = new LigneCommand
                {
                    date = DateTime.Today,
                    ProduitId = id,
                    quantite = 1,
                    panier = panier
                };

                ligneCommandService.Add(ligneCommand);
                ligneCommandService.Commit();

            }
            else
            {
                ligneCommandService.Increment(panier.idPanier, p.IdProduct);

            }



        }
        [HttpGet]
        public void DeleteLigne(int idl)
        {
            ligneCommandService.DeleteLigne(idl);
        }
        [HttpGet]
        public void Increment(int id)
        {
            ligneCommandService.IncrementLigne(id);

        }
        [HttpGet]
        public void Decrement(int id)
        {
            ligneCommandService.DecrementLigne(id);

        }
        [HttpGet]
        public int GetNumberLineApi()
        {
            return ligneCommandService.GetByPanier(panier.idPanier);
        }
        [HttpGet]
        public Double GetPrice()
        {
            return ligneCommandService.PanierPrice(panier.idPanier);
        }
        [HttpGet]
        public void DeleteAll()
        {
            ligneCommandService.DeleteALL(panier.idPanier);
            ligneCommandService.Commit();
        }

        // GET: LigneCommand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LigneCommand/Create
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


        // GET: LigneCommand/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LigneCommand/Edit/5
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

        // GET: LigneCommand/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LigneCommand/Delete/5
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
