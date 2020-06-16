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

namespace Solution.Web.Controllers
{
    public class RayonsController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/Rayons
        public IQueryable<Rayon> GetRayon()
        {
            return db.Rayons;
        }

        // GET: api/Rayons/5
        [ResponseType(typeof(Rayon))]
        public IHttpActionResult GetRayon(int id)
        {
            Rayon rayon = db.Rayons.Find(id);
            if (rayon == null)
            {
                return NotFound();
            }

            return Ok(rayon);
        }

        // PUT: api/Rayons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRayon(int id, Rayon rayon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rayon.IdRayon)
            {
                return BadRequest();
            }

            db.Entry(rayon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RayonExists(id))
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

        // POST: api/Rayons
        [ResponseType(typeof(Rayon))]
        public IHttpActionResult PostRayon(Rayon rayon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rayons.Add(rayon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rayon.IdRayon }, rayon);
        }

        // DELETE: api/Rayons/5
        [ResponseType(typeof(Rayon))]
        public IHttpActionResult DeleteRayon(int id)
        {
            Rayon rayon = db.Rayons.Find(id);
            if (rayon == null)
            {
                return NotFound();
            }

            db.Rayons.Remove(rayon);
            db.SaveChanges();

            return Ok(rayon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RayonExists(int id)
        {
            return db.Rayons.Count(e => e.IdRayon == id) > 0;
        }
    }
}