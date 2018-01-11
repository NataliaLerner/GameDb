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
    public class CharactersController : Controller
    {
        private GameDbEntities db = new GameDbEntities();

        // GET: Characters
        public ActionResult Index()
        {
            var characters = db.Characters.Include(c => c.Guilds).Include(c => c.Users);
            return View(characters.ToList());
        }

        // GET: Characters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Characters characters = db.Characters.Find(id);
            if (characters == null)
            {
                return HttpNotFound();
            }
            return View(characters);
        }

        // GET: Characters/Create
        public ActionResult Create()
        {
            ViewBag.guild_id = new SelectList(db.Guilds, "guild_id", "name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "login");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "character_id,user_id,nickname,date_created,guild_id")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                db.Characters.Add(characters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guild_id = new SelectList(db.Guilds, "guild_id", "name", characters.guild_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "login", characters.user_id);
            return View(characters);
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Characters characters = db.Characters.Find(id);
            if (characters == null)
            {
                return HttpNotFound();
            }
            ViewBag.guild_id = new SelectList(db.Guilds, "guild_id", "name", characters.guild_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "login", characters.user_id);
            return View(characters);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "character_id,user_id,nickname,date_created,guild_id")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guild_id = new SelectList(db.Guilds, "guild_id", "name", characters.guild_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "login", characters.user_id);
            return View(characters);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Characters characters = db.Characters.Find(id);
            if (characters == null)
            {
                return HttpNotFound();
            }
            return View(characters);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Characters characters = db.Characters.Find(id);
            db.Characters.Remove(characters);
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
