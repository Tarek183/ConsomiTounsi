using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class WishesController : Controller
    {
        //private MyContext db = new MyContext();
        //to get the user
        private ApplicationDbContext applicationDb = new ApplicationDbContext();
        IWishService service;
        IKidService KidService;
        IFundRaiserService FundRaiserService;

        public WishesController()
        {
            service = new WishService();
            KidService = new KidService();
            FundRaiserService = new FundRaiserService();
        }

        // GET: Wishes
        public ActionResult Index()
        {
            //PopulateDB();
            var wishes = service.GetMany();

            //hand the diagnosis to the view 
            //to filter 
            ViewBag.diagnosis = GetDiagnosis();
            return View(wishes.ToList());
        }

        //display wishes of the fundraiser x 
        //the id is the user id 
        //TODO: adding a user id column to the fundraiser
        [Authorize]
        public ActionResult WishesFundRaisedByAFundRaiser()
        {
            //get the id of the current fundraiser 
            //look for the fundraiser by email which is unique
            var UserId = User.Identity.GetUserId();
            var user = applicationDb.Users.FirstOrDefault(u => u.Id == UserId);
            var fundraiser = FundRaiserService.Get(f => f.Email.Equals(user.Email));



            //to test 
            //fundraiserid=123
            var wishes = service.GetMany(w => w.Kid.FundRaiserID == fundraiser.FundRaiserID);


            if (wishes.Any())
            {
                wishes = wishes.OrderBy(w => w.ExpirationDate).Reverse();
                return View(wishes.ToList());


            }

            return RedirectToAction("NotFound");


        }

        //display the notfound page 
        public ActionResult NotFound()
        {

            return View();
        }


        public ActionResult About()
        {

            return View();
        }


        // GET: Wishes/Details/5
        public ActionResult Details(int id)
        {

            Wish wish = service.GetById(id);
            if (wish == null)
            {
                return HttpNotFound();
            }

            //getting to percentage of the fund raised to show it in the progress bar 
            double FundRaisedPercentage = ((double)wish.FundRaised / wish.FundToRaise) * 100;
            ViewBag.ProgressFund = FundRaisedPercentage;
            Kid kid = wish.Kid;
            ViewBag.expirationDays = (wish.ExpirationDate - DateTime.Now).Days;
            if (kid.RelationshipToChild == Referer.Relative)
            {
                ViewBag.fundraiser = "My " + kid.RelationSpecification;
            }
            else
            {
                ViewBag.fundraiser = "The Organisation" + kid.OrganizationName;
            }

            return View(wish);
        }

        [Authorize]
        // GET: Wishes/Create/kidId
        public ActionResult Create(int id)
        {
            int kidId = id;

            //display the firstname in the view
            string KidFirstName = KidService.
                GetById(kidId).
                KidFistName;

            //initialise the wish with the kid id 
            Wish wish = new Wish
            {
                KidID = kidId
            };

            ViewBag.KidFirstName = KidFirstName;
            return View(wish);
        }

        // POST: Wishes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Wish wish)
        {
            if (ModelState.IsValid)
            {
                //adding a prefix to the wish string
                //string TheWish = "I wish to " + wish.KidWish;


                service.Add(wish);
                service.Commit();



                // return RedirectToAction("Details" , new { id = wish.WishID});
                return RedirectToAction("WishesFundRaisedByAFundRaiser");
            }

            ViewBag.KidID = new SelectList(KidService.GetMany(), "KidID", "KidFistName", wish.KidID);
            return View(wish);
        }

        [Authorize]
        // GET: Wishes/Edit/5
        public ActionResult Edit(int id)
        {

            Wish wish = service.GetById(id);
            if (wish == null)
            {
                return HttpNotFound();
            }
            ViewBag.KidID = new SelectList(KidService.GetMany(), "KidID", "KidFistName", wish.KidID);
            return View(wish);
        }

        // POST: Wishes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Wish wish)
        {

            if (ModelState.IsValid)
            {
                //get the wish to update
                var wishToUpdate = service.GetById(wish.WishID);

                //add the updated attributes
                wishToUpdate.ExpirationDate = wish.ExpirationDate;
                wishToUpdate.FundToRaise = wish.FundToRaise;
                wishToUpdate.Desc = wish.Desc;
                wishToUpdate.KidWish = wish.KidWish;
                //update db
                service.Update(wishToUpdate);
                service.Commit();
                return RedirectToAction("Edit", "Kids", new { id = wishToUpdate.KidID });
                //return RedirectToAction("Index");
            }
            ViewBag.KidID = new SelectList(KidService.GetMany(), "KidID", "KidFistName", wish.KidID);
            return View(wish);
        }

        // GET: Wishes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Wish wish = db.Wishes.Find(id);
        //    if (wish == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(wish);
        //}


        //the wish is deleted using ajax
        // POST: Wishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        // [ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {

            //delete the wish
            Wish wish = service.GetById(id);
            service.Delete(wish);
            service.Commit();

            //delete the kid 
            Kid kid = KidService.GetById(wish.KidID);
            KidService.Delete(kid);
            KidService.Commit();

            return Json("wish deleted");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public void PopulateDB()
        {


            for (int i = 0; i < 10; i++)
            {



                Wish w = new Wish
                {
                    Desc = "For Nicol life with biliary atresia has meant a childhood very different from her peers.Hospital visits and exams replaced normal childhood activities, and she was focused on battling her critical illness.",
                    ExpirationDate = DateTime.Parse("07-07-2020"),
                    FundToRaise = 500,
                    KidWish = "Swiming with the whales",
                    KidID = 28 + i,
                    // Kid = KidService.GetById(5 + i),



                };


                service.Add(w);
                service.Commit();
            }

        }


        //get all the diagnosis
        public List<string> GetDiagnosis()
        {
            var kids = KidService.GetMany().ToList();
            List<string> diagnosis = new List<string>();
            string kidDiagnosis;
            foreach (var kid in kids)
            {
                kidDiagnosis = kid.KidDiagnosis;
                if (!diagnosis.Contains(kidDiagnosis))
                    diagnosis.Add(kidDiagnosis);
            }

            return diagnosis;
        }
    }

}