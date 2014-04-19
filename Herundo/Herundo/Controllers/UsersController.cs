using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Herundo.Controllers
{
    public class UsersController : Controller
    {
        static IRepository db = new MongoRepository();
        //
        // GET: /Users/
        [Authorize]
        public ActionResult Index()
        {
            //db.AddFollowing(User.Identity.Name, "test2");
            List<User> allUsers = db.GetAllUsers();
            User currUser = db.getUserByUsername(User.Identity.Name);
            ViewBag.AllUsers = allUsers;
            return View(currUser);
        }

        //
        // GET: /Users/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }


        [Authorize]
        public ActionResult Follow(string username)
        {
            db.AddFollowing(User.Identity.Name, username);
            return Redirect("/Users");
        }

        [Authorize]
        public ActionResult Unfollow(string username)
        {
            db.StopFollowing(User.Identity.Name, username);
            return Redirect("/Users");
        }

        //
        // POST: /Users/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Users/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Users/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Users/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Users/Delete/5
        [Authorize]
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
    }
}
