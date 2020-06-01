using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class CommentController : Controller
    {
        ICommentService CS;
        IUserService US;
        IPostService PS;
        public CommentController()
        {
            CS = new CommentService();
            US = new UserService();
            PS = new PostService();
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View(CS.GetMany());
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            string UserName = US.GetById(userId).UserName;
            string Phone = US.GetById(userId).Phone;
            string mail = US.GetById(userId).Email;
            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;
            ViewBag.authenticated = val1;
            return View();        
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(PostComment pc, int id)
        {
            TempData["message"] = "Added";
            Post p = new Post();
            id = p.PostId;
            Comment c = new Comment() 
            {
              ContentComment = pc.ContentComment,
              CommentDate = DateTime.Now,
              LikeComment = 0,
              DislikeComment = 0,
              PostId = id,
            };
            c.UserId = User.Identity.GetUserId<string>();
            CS.Add(c);
            CS.Commit();
            return RedirectToAction("Index", "Post");
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
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
        public ActionResult Edit(Comment c)
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

        //  GET: Comment/Delete/5
        public ActionResult Delete(int id)
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
            c.UserId = User.Identity.GetUserId();
            //CS.Delete(c);
            //CS.Commit();
            return RedirectToAction("Index");
        }


        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            Comment c = CS.GetById(id);
            CS.Delete(c);
            CS.Commit();
            return RedirectToAction("Index");

        }

    }
}
