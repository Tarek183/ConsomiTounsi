using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers.Api
{
    public class DonationsController : ApiController
    {


        //to get the user
        //private ApplicationDbContext applicationDb = new ApplicationDbContext();
        IDonationService service;
        IWishService wishService;
        private MyContext db = new MyContext();



        public DonationsController()
        {
            service = new DonationService();
            wishService = new WishService();


        }



        //GET /api/donations
        public IHttpActionResult GetDonations()
        {

            IList<Donation> donations = new List<Donation>();
            var userId = User.Identity.GetUserId();
            //var user = US.GetById(userId);
            //var userId = db.users.First().Id;
            foreach (var item in service.GetMany(d => d.UserId == userId))
            {
                Donation donation = new Donation()
                {
                    ID = item.ID,
                    Date = item.Date,
                    Sum = item.Sum,
                    UserId = item.UserId,
                    WishId = item.WishId,

                };


                donations.Add(donation);
            }
            if (donations.Count == 0)
            {
                return NotFound();
            }

            return Ok(donations);
        }



        // POST: api/donations
        public IHttpActionResult Post(Donation item)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            //if it is not working create  a wish instance 
            Donation donation = new Donation()
            {
                //ID = item.ID,
                Date = item.Date,
                Sum = item.Sum,
                UserId = User.Identity.GetUserId(),
                WishId = item.WishId,



            };
            service.Add(donation);
            service.Commit();

            //update the wish
            Wish wish = wishService.GetById(item.WishId);
            wish.FundRaised += item.Sum;
            wishService.Update(wish);
            wishService.Commit();

            return Ok();
        }

        //PUT /api/wishes/5
        public IHttpActionResult Put(Donation donation)
        {
            if (ModelState.IsValid)
            {
                //get the wish to update
                var DonationToUpdate = service.GetById(donation.ID);

                //add the updated attributes
                DonationToUpdate.Date = DateTime.Now;
                DonationToUpdate.Sum = donation.Sum;



                service.Update(DonationToUpdate);
                service.Commit();

                //update the wish
                Wish wish = wishService.GetById(donation.WishId);
                wish.FundRaised -= DonationToUpdate.Sum;
                wish.FundRaised += donation.Sum;
                wishService.Update(wish);
                wishService.Commit();


                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/donations/5
        public IHttpActionResult Delete(int id)
        {
            //delete the wish
            Donation donation = service.GetById(id);
            service.Delete(donation);
            service.Commit();



            //update the wish
            Wish wish = wishService.GetById(donation.WishId);
            wish.FundRaised -= donation.Sum;
            wishService.Update(wish);
            wishService.Commit();

            return Ok();
        }


    }

}