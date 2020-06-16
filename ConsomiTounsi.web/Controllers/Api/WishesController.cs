using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers.Api
{
    public class WishesController : ApiController
    {

        //to get the user
        //private ApplicationDbContext applicationDb = new ApplicationDbContext();
        IWishService service;
        IKidService KidService;
        IFundRaiserService FundRaiserService;

        public WishesController()
        {
            service = new WishService();
            KidService = new KidService();
            FundRaiserService = new FundRaiserService();
        }



        //GET /api/wishes
        public IHttpActionResult GetWishes()
        {

            IList<Wish> wishes = new List<Wish>();
            foreach (var item in service.GetMany())
            {
                Wish wish = new Wish()
                {
                    WishID = item.WishID,
                    //Kid = item.Kid,
                    KidID = item.KidID,
                    Desc = item.Desc,
                    ExpirationDate = item.ExpirationDate,
                    FundRaised = item.FundRaised,
                    FundToRaise = item.FundToRaise

                };


                wishes.Add(wish);
            }
            if (wishes.Count == 0)
            {
                return NotFound();
            }

            return Ok(wishes);
        }


        // GET: api/wishes/5
        public IHttpActionResult Get(int id)
        {
            Wish item = service.GetById(id);

            if (item != null)
            {
                Wish wish = new Wish()
                {
                    WishID = item.WishID,
                    //Kid = item.Kid,
                    KidID = item.KidID,
                    Desc = item.Desc,
                    ExpirationDate = item.ExpirationDate,
                    FundRaised = item.FundRaised,
                    FundToRaise = item.FundToRaise
                };

                return Ok(wish);
            }

            return NotFound();
        }

        // POST: api/wishes
        public IHttpActionResult Post(Wish item)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            //if it is not working create  a wish instance 
            Wish wish = new Wish()
            {
                WishID = item.WishID,
                //Kid = item.Kid,
                KidID = item.KidID,
                Desc = item.Desc,
                ExpirationDate = item.ExpirationDate,
                FundRaised = item.FundRaised,
                FundToRaise = item.FundToRaise
            };
            service.Add(wish);
            service.Commit();

            return Ok();
        }

        //PUT /api/wishes/5
        public IHttpActionResult Put(Wish wish)
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

                service.Update(wishToUpdate);
                service.Commit();
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/wishes/5
        public IHttpActionResult Delete(int id)
        {
            //delete the wish
            Wish wish = service.GetById(id);
            service.Delete(wish);
            service.Commit();

            ////delete the kid 
            //Kid kid = KidService.GetById(wish.KidID);
            //KidService.Delete(kid);
            //KidService.Commit();

            return Ok();
        }

    }

}