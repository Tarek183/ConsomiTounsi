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
using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.Service;
using Solution.Service;

namespace ConsomiTounsi.web.Controllers
{
    public class LigneCommandsApiController : ApiController
    {
        private MyContext db = new MyContext();
        public ILigneCommandService ligneCommandService;
        public LigneCommandsApiController()
        {
            ligneCommandService = new LigneCommandService(); 
        }

        // GET: api/LigneCommandsApi
        public String  GetLigneCommands()
        {
           return ligneCommandService.GetMany().ToString(); 
           
        }

        // GET: api/LigneCommandsApi/5
        [ResponseType(typeof(LigneCommand))]
        public IHttpActionResult GetLigneCommand(int id)
        {
            LigneCommand ligneCommand = db.LigneCommands.Find(id);
            if (ligneCommand == null)
            {
                return Ok(new LigneCommand()) ;
            }

            return Ok(ligneCommand);
        }

        // PUT: api/LigneCommandsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLigneCommand(int id, LigneCommand ligneCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ligneCommand.idLigneCommand)
            {
                return BadRequest();
            }

            db.Entry(ligneCommand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LigneCommandExists(id))
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

        // POST: api/LigneCommandsApi
        [ResponseType(typeof(LigneCommand))]
        public IHttpActionResult PostLigneCommand(LigneCommand ligneCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LigneCommands.Add(ligneCommand);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ligneCommand.idLigneCommand }, ligneCommand);
        }

        // DELETE: api/LigneCommandsApi/5
        [ResponseType(typeof(LigneCommand))]
        public IHttpActionResult DeleteLigneCommand(int id)
        {
            LigneCommand ligneCommand = db.LigneCommands.Find(id);
            if (ligneCommand == null)
            {
                return NotFound();
            }

            db.LigneCommands.Remove(ligneCommand);
            db.SaveChanges();

            return Ok(ligneCommand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LigneCommandExists(int id)
        {
            return db.LigneCommands.Count(e => e.idLigneCommand == id) > 0;
        }
    }
}