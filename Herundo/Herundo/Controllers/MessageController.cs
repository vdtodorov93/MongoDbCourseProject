using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Herundo.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        //
        // GET: /Message/
        static IRepository db = new MongoRepository();
        [Authorize]
        public ActionResult Index()
        {
            //List<Message> lastMessages = db.LastMessages(User.Identity.Name, 2);
            List<Message> lastMessages = db.GetLastFewMessages(User.Identity.Name, 3);
            ViewBag.LastMessages = lastMessages;
            return View();
        }

        //
        // GET: /Message/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Message/Create
        [Authorize]
        public ActionResult Create()
        {
            //List<Message> tweets = db.LastMessages(User.Identity.Name, 3);
            return View();
        }

        //
        // POST: /Message/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Herundo.Models.MessageModel model)
        {
            try
            {
                if (model.Content.Length > 140)
                {
                    throw new Exception("Message can't be longer than 140 symbols");
                }
                Message newMsg = new Message(User.Identity.Name, model.Content, DateTime.Now, model.Location);
                db.AddTweet(newMsg);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Message/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Message/Edit/5
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
        // GET: /Message/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Message/Delete/5
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
