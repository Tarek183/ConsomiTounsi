using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ConsomiTounsi.web.Controllers
{
    public class PaniersApiController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/Paniers
        public IQueryable<Panier> GetPaniers()
        {
            return db.Paniers;
        }

        // GET: api/Paniers/5
        [ResponseType(typeof(Panier))]
        public IHttpActionResult GetPanier(int id)
        {
            Panier panier = db.Paniers.Find(id);
            if (panier == null)
            {
                return NotFound();
            }

            return Ok(panier);
        }

        // PUT: api/Paniers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPanier(int id, Panier panier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != panier.idPanier)
            {
                return BadRequest();
            }

            db.Entry(panier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Paniers
        [ResponseType(typeof(Panier))]
        public IHttpActionResult PostPanier(Panier panier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paniers.Add(panier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = panier.idPanier }, panier);
        }

        // DELETE: api/Paniers/5
        [ResponseType(typeof(Panier))]
        public IHttpActionResult DeletePanier(int id)
        {
            Panier panier = db.Paniers.Find(id);
            if (panier == null)
            {
                return NotFound();
            }

            db.Paniers.Remove(panier);
            db.SaveChanges();

            return Ok(panier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PanierExists(int id)
        {
            return db.Paniers.Count(e => e.idPanier == id) > 0;
        }
    }
}