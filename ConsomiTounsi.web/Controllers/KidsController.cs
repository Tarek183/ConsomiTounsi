using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;



namespace ConsomiTounsi.web.Controllers
{
    //[Authorize]
    public class KidsController : Controller
    {
        private MyContext db = new MyContext();
        IKidService service;
        IFundRaiserService FundRaiserService;



        public KidsController()
        {
            service = new KidService();
            FundRaiserService = new FundRaiserService();
        }
        // GET: Kids
        public ActionResult Index()
        {
            PopulateDB();
            var kids = db.Kids.Include(k => k.FundRaiser);
            return View(kids.ToList());
        }

        // GET: Kids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kid kid = db.Kids.Find(id);
            if (kid == null)
            {
                return HttpNotFound();
            }
            return View(kid);
        }

        // GET: Kids/Create
        public ActionResult Create(int id)
        {
            Kid kid = new Kid();
            var FundRaiserId = id;
            //kid.FundRaiserID = db.Fundraisers.
            //    Where(f => f.FundRaiserID == FundRaiserId).
            //    Select(f => f.FundRaiserID).
            //    Single();
            kid.FundRaiserID = FundRaiserService.
                GetById(FundRaiserId).
                FundRaiserID;

            return View(kid);
        }

        // POST: Kids/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kid kid)
        {

            //check if the fundraiser already refered to this kid 
            // the first name , last name , diagnosis and  fundraiser id 
            // TODO: check if it is working or not 
            //FundRaiser fundRaiser = FundRaiserService.GetById(kid.FundRaiserID);
            //var checkKidUnique = fundRaiser.Kids.Contains(kid) ;


            if (ModelState.IsValid /*&& checkKidUnique*/)
            {
                //calculate the age
                int age = DateTime.Today.Year - kid.KidBirthdate.Year;
                kid.Age = age;

                service.Add(kid);
                service.Commit();
                //service.Dispose();



                return RedirectToAction("Create", "Wishes", new { id = kid.KidID });
            }

            ViewBag.FundRaiserID = new SelectList(db.Fundraisers, "FundRaiserID", "Email", kid.FundRaiserID);
            ViewBag.Message = "you already refered to this kid";
            return View(kid);
        }

        // GET: Kids/Edit/5
        public ActionResult Edit(int id)
        {
            Kid kid = service.GetById(id);
            if (kid == null)
            {
                return HttpNotFound();
            }
            ViewBag.FundRaiserID = new SelectList(db.Fundraisers, "FundRaiserID", "Email", kid.FundRaiserID);
            return View(kid);
        }

        // POST: Kids/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kid kid)
        {
            if (ModelState.IsValid)
            {
                var KidToUpdate = service.GetById(kid.KidID);

                KidToUpdate.KidDiagnosis = kid.KidDiagnosis;
                KidToUpdate.KidFistName = kid.KidFistName;
                KidToUpdate.KidSecondName = kid.KidSecondName;
                KidToUpdate.KidBirthdate = kid.KidBirthdate;
                KidToUpdate.MedicalProfessionalEmail = kid.MedicalProfessionalEmail;
                KidToUpdate.RelationshipToChild = kid.RelationshipToChild;
                kid.RelationSpecification = kid.RelationSpecification;
                kid.OrganizationName = kid.OrganizationName;

                service.Update(KidToUpdate);
                service.Commit();

                return RedirectToAction("Index", "Wishes");
            }
            ViewBag.FundRaiserID = new SelectList(db.Fundraisers, "FundRaiserID", "Email", kid.FundRaiserID);
            return View(kid);
        }

        // GET: Kids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kid kid = db.Kids.Find(id);
            if (kid == null)
            {
                return HttpNotFound();
            }
            return View(kid);
        }

        // POST: Kids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kid kid = db.Kids.Find(id);
            db.Kids.Remove(kid);
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


                Kid kid = new Kid
                {
                    Age = i,
                    KidDiagnosis = "Stomach Cancer",
                    KidFistName = "Jack" + i,
                    FundRaiserID = 5,
                    //FundRaiser = FundRaiserService.GetById(5),
                    KidSecondName = "Sunday" + i,
                    MedicalProfessionalEmail = i + "@gmail.com",


                };


                service.Add(kid);
                service.Commit();
            }

        }
    }

}