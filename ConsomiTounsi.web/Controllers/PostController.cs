using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.Models;
using ConsomiTounsi.web.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class PostController : Controller
    {
        IPostService PS;
        ICommentService CS;
        IUserService US;
        MyContext ctx = new MyContext();
        public PostController()
        {
            PS = new PostService();
            CS = new CommentService();
            US = new UserService();
        }
        // GET: Post
        public ActionResult Index()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            string image = US.GetById(userId).image;
            string role = US.GetById(userId).Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.image = image;
            ViewBag.Role = role;
            ViewBag.authenticated = val1;
            
            var mymodel = new PostComment();
            mymodel.Posts = ctx.Posts.ToList();
            mymodel.Comments = ctx.Comments.ToList();


            return View(mymodel);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //public ActionResult AddComment(int id)
        //{
        //    return RedirectToAction("Create", "Comment", new { CommentId = id });
        //}

        // GET: Post/Create
        public ActionResult Create()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();

            var userId = User.Identity.GetUserId();
            var user = US.GetById(userId);

            string UserName = user.UserName;
            string Phone = user.Phone;
            string mail = user.Email;
            string role = user.Role;
            string image = user.image;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.Role = role;
            ViewBag.authenticated = val1;

            var mymodel = new PostComment();
            mymodel.User = user;
            mymodel.Posts = ctx.Posts.ToList();
            mymodel.Comments = ctx.Comments.ToList();
            return View(mymodel);
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(PostComment pc, HttpPostedFileBase file)
        {
            Post p = new Post();

            p.Content = pc.Content;
            p.PublishDate = DateTime.Today.Date;
            p.UrlImage = file.FileName;
            var fileName = "";
            if (file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/Content/uploads/"), file.FileName);
                file.SaveAs(path);
            }
            p.UserId = User.Identity.GetUserId<string>();
            PS.Add(p);
            PS.Commit();
                return RedirectToAction("Index");
        }



        //// GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            string role = US.GetById(userId).Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.Role = role;
            ViewBag.authenticated = val1;

            Post p = PS.GetById(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(Post p, HttpPostedFileBase file)
        {
            Post post = PS.GetById(p.PostId);
            p.UserId = User.Identity.GetUserId<string>();

            post.Content = p.Content;
            post.PublishDate = DateTime.Now;
            post.UrlImage = file.FileName;
            var fileName = "";

            if (file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                file.SaveAs(path);
            }
            if (ModelState.IsValid)
            {
                PS.Update(post);
                PS.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }

        //  GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post p = PS.GetById(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            PS.Delete(p);
            PS.Commit();
            return RedirectToAction("Index");
            //return View(p);

        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateComment()
        {
            ViewBag.PostId = new SelectList(ctx.Posts, "PostId", "Content");
            return View("Index");
        }
        // POST: Comment/Create
        [HttpPost]
        public ActionResult CreateComment(PostComment pc)
        {
            Comment c = new Comment()
            {
                ContentComment = pc.ContentComment,
                CommentDate = DateTime.Now,
                LikeComment = 0,
                DislikeComment = 0,
                PostId = pc.PostId
            };
            c.UserId = User.Identity.GetUserId<string>();
            CS.Add(c);
            CS.Commit();
            return RedirectToAction("Index", "Post");
        }


        // GET: Comment/Edit/5
        public ActionResult EditComment(int id)
        {
            Comment c = CS.GetById(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult EditComment(Comment c)
        {
            Comment comment = CS.GetById(c.CommentId);
            c.UserId = User.Identity.GetUserId<string>();

            //c.PostId = PS.GetById(p.PostId);
            try
            {
                comment.ContentComment = c.ContentComment;
                CS.Update(comment);
                CS.Commit();
                return RedirectToAction("Index", "Post");
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
            return View();
        }

        public ActionResult DeleteComment(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment c = CS.GetById(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            //c.UserId = User.Identity.GetUserId();
            CS.Delete(c);
            CS.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult Like(int id)
        {
            Post update = ctx.Posts.ToList().Find(u => u.PostId == id);
            //PS.GetMany();
            //Post update = PS.Get(u => u.PostId == id);

            update.Like += 1;
            if (update.DisLike > 0)
            {
                update.DisLike -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DisLike(int id)
        {
            Post update = ctx.Posts.ToList().Find(u => u.PostId == id);

            update.DisLike += 1;
            if (update.Like > 0)
            {
                update.Like -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LikeComment(int id)
        {
            Comment update = ctx.Comments.ToList().Find(u => u.CommentId == id);
            //PS.GetMany();
            //Post update = PS.Get(u => u.PostId == id);

            update.LikeComment += 1;
            if (update.DislikeComment > 0)
            {
                update.DislikeComment -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DisLikeComment(int id)
        {
            Comment update = ctx.Comments.ToList().Find(u => u.CommentId == id);

            update.DislikeComment += 1;
            if (update.LikeComment > 0)
            {
                update.LikeComment -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        //**************************************************
        public ActionResult LikeCreate(int id)
        {
            Post update = ctx.Posts.ToList().Find(u => u.PostId == id);
            //PS.GetMany();
            //Post update = PS.Get(u => u.PostId == id);

            update.Like += 1;
            if (update.DisLike > 0)
            {
                update.DisLike -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Create");
        }
        public ActionResult DisLikeCreate(int id)
        {
            Post update = ctx.Posts.ToList().Find(u => u.PostId == id);

            update.DisLike += 1;
            if (update.Like > 0)
            {
                update.Like -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Create");
        }

        public ActionResult CreateCommentC()
        {
            ViewBag.PostId = new SelectList(ctx.Posts, "PostId", "Content");
            return View("Create");
        }
        // POST: Comment/Create
        [HttpPost]
        public ActionResult CreateCommentC(PostComment pc)
        {
            //Post post = ctx.Posts.Find(id);
            //Post post = ctx.Posts.ToList().Find(u => u.PostId == id);
            // Post id = PS.GetById(pc.PostId);
            Comment c = new Comment()
            {
                ContentComment = pc.ContentComment,
                CommentDate = DateTime.Now,
                LikeComment = 0,
                DislikeComment = 0,
                PostId = pc.PostId
            };
            //ViewBag.PostId = new SelectList(ctx.Posts, "PostId", "Content", pc.PostId);
            c.UserId = User.Identity.GetUserId<string>();
            CS.Add(c);
            CS.Commit();
            return RedirectToAction("Create", "Post");
        }

        public ActionResult LikeCommentC(int id)
        {
            Comment update = ctx.Comments.ToList().Find(u => u.CommentId == id);
            //PS.GetMany();
            //Post update = PS.Get(u => u.PostId == id);

            update.LikeComment += 1;
            if (update.DislikeComment > 0)
            {
                update.DislikeComment -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Create");
        }
        public ActionResult DisLikeCommentC(int id)
        {
            Comment update = ctx.Comments.ToList().Find(u => u.CommentId == id);

            update.DislikeComment += 1;
            if (update.LikeComment > 0)
            {
                update.LikeComment -= 1;
            }
            ctx.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}
