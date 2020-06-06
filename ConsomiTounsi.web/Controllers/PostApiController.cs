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
        IPostService PostServ;
        //private AnnonceService ms = new AnnonceService();
        List<PostModel> result = new List<PostModel>();
        public PostApiController()
        {
            PostServ = new PostService();
            Index();
            result = Index().ToList();
        }

        public List<PostModel> Index()
        {
            IEnumerable<Post> Posts = PostServ.GetMany();
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
       
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(result);
        }
        // GET api/PostApi/5

        public IHttpActionResult Get(int id)
        {
            Post post = PostServ.GetById(id);
            Post ndPost = new Post
            {
                Content = post.Content,
                PostId = post.PostId,
                PublishDate = post.PublishDate,
                DisLike = post.DisLike
            };
            return Json(ndPost);
        }

        // POST a
        [HttpPost]
        public IHttpActionResult Post(Post post)
        {
            using (var ctx = new MyContext())
            {
                ctx.Posts.Add(post);
                ctx.SaveChanges();
            }
                
            return Ok();
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
            Post post = PostServ.GetById(id);
            PostServ.Delete(post);
            PostServ.Commit();
            return Ok(post);
        }
    }
}