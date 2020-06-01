using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ConsomiTounsi.web.Controllers
{
    public class CommentApiController : ApiController
    {
        ICommentService CommentServ;
        List<CommentModel> result = new List<CommentModel>();
        public CommentApiController()
        {
            CommentServ = new CommentService();
            Index();
            result = Index().ToList();
        }
        public List<CommentModel> Index()
        {
            IEnumerable<Comment> Comments = CommentServ.GetMany();
            List<CommentModel> CommentsXml = new List<CommentModel>();
            foreach (Comment c in Comments)
            {
                CommentsXml.Add(new CommentModel
                {
                    CommentId = c.CommentId,
                    CommentDate = c.CommentDate,
                    ContentComment = c.ContentComment,
                    LikeComment = c.LikeComment,
                    DislikeComment = c.DislikeComment,
                    //UserId = c.UserId
                });
            }
            return CommentsXml;
        }
        // GET api/<controller>
        public IEnumerable<CommentModel> Get()
        {
            return result;
        }

        // GET api/<controller>/5
        public Comment Get(int id)
        {
            Comment comment = CommentServ.GetById(id);
            return comment;
        }

        // POST api/<controller>
        public Comment Post(Comment comment)
        {
            CommentServ.Add(comment);
            CommentServ.Commit();
            return comment;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
        // PUT: api/CommentApi/5
        [HttpPut]
        public IHttpActionResult Put(CommentModel cm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existingFormation = ctx.Comments.Where(c => c.CommentId == cm.CommentId)
                                                        .FirstOrDefault<Comment>();

                if (existingFormation != null)
                {
                    existingFormation.CommentId = cm.CommentId;
                    existingFormation.CommentDate = DateTime.UtcNow;
                    existingFormation.LikeComment = cm.LikeComment;
                    existingFormation.DislikeComment = cm.DislikeComment;
                    existingFormation.ContentComment = cm.ContentComment;
                    existingFormation.UserId = cm.UserId;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Comment comment = CommentServ.GetById(id);
            CommentServ.Delete(comment);
            CommentServ.Commit();
            return Ok(comment);
        }
    }
}