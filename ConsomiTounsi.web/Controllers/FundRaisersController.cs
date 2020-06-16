using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class FundRaisersController : Controller
    {
        private MyContext db = new MyContext();

        //to get the user
        private ApplicationDbContext applicationDb = new ApplicationDbContext();
        IFundRaiserService service;



        public FundRaisersController()
        {
            service = new FundRaiserService();
        }

        // GET: FundRaisers
        public ActionResult Index()
        {
            return View(db.Fundraisers.ToList());
        }

        // GET: FundRaisers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FundRaiser fundRaiser = db.Fundraisers.Find(id);
            if (fundRaiser == null)
            {
                return HttpNotFound();
            }
            return View(fundRaiser);
        }

        // GET: FundRaisers/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {

                //look for the fundraiser by email which is unique
                var UserId = User.Identity.GetUserId();
                var user = applicationDb.Users.FirstOrDefault(u => u.Id == UserId);
                var fundraiser = service.Get(f => f.Email.Equals(user.Email));

                //update CIN
                if (fundraiser.CIN != 0)
                {
                    return RedirectToAction("Create", "Kids", new { id = fundraiser.FundRaiserID });
                }

                return View(fundraiser);
            }

            return RedirectToAction("Login", "Account");

        }

        // POST: FundRaisers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FundRaiser fundRaiser)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            Trace.WriteLine(errors);

            //validate the cin field only 
            //validate the full form throws an error 
            if (ModelState.IsValidField(fundRaiser.CIN.ToString()))
            {
                //if he dosen't have an cin yet , 
                //add the cin to the entity 
                //now the user is officially a fundraiser 
                FundRaiser FundraiserToUpdate = service.GetById(fundRaiser.FundRaiserID);
                FundraiserToUpdate.CIN = fundRaiser.CIN;
                service.Update(FundraiserToUpdate);
                service.Commit();

                return RedirectToAction("Create", "Kids", new { id = fundRaiser.FundRaiserID });
            }

            ViewBag.errors = errors;
            return View(fundRaiser);
        }

        // GET: FundRaisers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FundRaiser fundRaiser = db.Fundraisers.Find(id);
            if (fundRaiser == null)
            {
                return HttpNotFound();
            }
            return View(fundRaiser);
        }

        // POST: FundRaisers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FundRaiserID,PhoneNumber,Email,Address,RelationshipToChild,RelationSpecification")] FundRaiser fundRaiser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fundRaiser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fundRaiser);
        }

        // GET: FundRaisers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FundRaiser fundRaiser = db.Fundraisers.Find(id);
            if (fundRaiser == null)
            {
                return HttpNotFound();
            }
            return View(fundRaiser);
        }

        // POST: FundRaisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FundRaiser fundRaiser = db.Fundraisers.Find(id);
            db.Fundraisers.Remove(fundRaiser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void PopulateDB()
        {


            for (int i = 0; i < 20; i++)
            {

                FundRaiser fr = new FundRaiser
                {
                    Address = "14bis",
                    BirthDate = DateTime.Today,
                    Email = i + "@gmail.com",
                    FirstName = "Albert" + i,
                    LastName = "Jack" + i,
                    //OrganizationName = "un enfant un espoir",
                    //RelationshipToChild = Referer.Nonprofit,
                    PhoneNumber = "34324234",

                };


                service.Add(fr);
                service.Commit();
            }

        }
    }

}