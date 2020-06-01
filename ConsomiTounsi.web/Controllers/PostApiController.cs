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
    public class PostApiController : ApiController
    {
        IPostService PS;
        //private AnnonceService ms = new AnnonceService();
        List<PostModel> result = new List<PostModel>();
        public PostApiController()
        {
            PS = new PostService();
            Index();
            result = Index().ToList();
        }

        public List<PostModel> Index()
        {
            IEnumerable<Post> Posts = PS.GetMany();
            List<PostModel> PostsXml = new List<PostModel>();
            foreach (Post p in Posts)
            {
                PostsXml.Add(new PostModel
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    PublishDate = p.PublishDate,
                    UrlImage = p.UrlImage,
                    Like = p.Like,
                    DisLike = p.DisLike,
                    UserId = p.UserId
                });
            }
            return PostsXml;
        }

        // GET: api/PostApi
        public IEnumerable<PostModel> Get()
        {
            return result;
        }

        // GET api/PostApi/5
        public Post Get(int id)
        {
            Post post = PS.GetById(id);
            return post;
        }

        // POST api/PostApi
        [HttpPost]
        public Post Post(Post post)
        {
            PS.Add(post);
            PS.Commit();
            return post;
        }

        // PUT api/PostApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // PUT: api/PostApi/5
        [HttpPut]
        public IHttpActionResult Put(PostModel pm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existingFormation = ctx.Posts.Where(p => p.PostId == pm.PostId)
                                                        .FirstOrDefault<Post>();

                if (existingFormation != null)
                {
                    existingFormation.PostId = pm.PostId;
                    existingFormation.Content = pm.Content;
                    existingFormation.PublishDate = DateTime.UtcNow;
                    existingFormation.Like = pm.Like;
                    existingFormation.DisLike = pm.DisLike;
                    existingFormation.UrlImage = pm.UrlImage;
                    existingFormation.UserId = pm.UserId;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }


        // DELETE: api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            Post post = PS.GetById(id);
            PS.Delete(post);
            PS.Commit();
            return Ok(post);
        }
    }
}