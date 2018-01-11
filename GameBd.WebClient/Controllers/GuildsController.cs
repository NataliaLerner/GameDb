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
    public class GuildsController : Controller
    {
        private GameDbEntities db = new GameDbEntities();

        // GET: Guilds
        public ActionResult Index()
        {
            return View(db.Guilds.ToList());
        }

        // GET: Guilds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guilds guilds = db.Guilds.Find(id);
            if (guilds == null)
            {
                return HttpNotFound();
            }
            return View(guilds);
        }

        // GET: Guilds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guilds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guild_id,name,level,date_created,message")] Guilds guilds)
        {
            guilds.date_created = DateTime.Now;
            guilds.level = 1;
            guilds.message = "Message of the day!";

            if (ModelState.IsValid)
            {
                db.Guilds.Add(guilds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guilds);
        }

        // GET: Guilds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guilds guilds = db.Guilds.Find(id);
            if (guilds == null)
            {
                return HttpNotFound();
            }
            return View(guilds);
        }

        // POST: Guilds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guild_id,name,level,date_created,message")] Guilds guilds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guilds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guilds);
        }

        // GET: Guilds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guilds guilds = db.Guilds.Find(id);
            if (guilds == null)
            {
                return HttpNotFound();
            }
            return View(guilds);
        }

        // POST: Guilds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guilds guilds = db.Guilds.Find(id);
            db.Guilds.Remove(guilds);
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
