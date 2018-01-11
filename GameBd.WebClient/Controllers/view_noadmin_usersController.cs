using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameBd.WebClient.Models;

namespace GameBd.WebClient.Controllers
{
    public class view_noadmin_usersController : Controller
    {
        private GameDbEntities db = new GameDbEntities();

        // GET: view_noadmin_users
        public ActionResult Index()
        {
            return View(db.view_noadmin_users.ToList());
        }

        // GET: view_noadmin_users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_noadmin_users view_noadmin_users = db.view_noadmin_users.Find(id);
            if (view_noadmin_users == null)
            {
                return HttpNotFound();
            }
            return View(view_noadmin_users);
        }

        // GET: view_noadmin_users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: view_noadmin_users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "login,firstname,lastname,email")] view_noadmin_users view_noadmin_users)
        {
            if (ModelState.IsValid)
            {
                db.view_noadmin_users.Add(view_noadmin_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_noadmin_users);
        }

        // GET: view_noadmin_users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_noadmin_users view_noadmin_users = db.view_noadmin_users.Find(id);
            if (view_noadmin_users == null)
            {
                return HttpNotFound();
            }
            return View(view_noadmin_users);
        }

        // POST: view_noadmin_users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "login,firstname,lastname,email")] view_noadmin_users view_noadmin_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_noadmin_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_noadmin_users);
        }

        // GET: view_noadmin_users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_noadmin_users view_noadmin_users = db.view_noadmin_users.Find(id);
            if (view_noadmin_users == null)
            {
                return HttpNotFound();
            }
            return View(view_noadmin_users);
        }

        // POST: view_noadmin_users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            view_noadmin_users view_noadmin_users = db.view_noadmin_users.Find(id);
            db.view_noadmin_users.Remove(view_noadmin_users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
