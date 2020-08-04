using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieProject;

namespace MovieProject.Controllers
{
    public class tblDiscsController : Controller
    {
        private dbMovieShopEntities db = new dbMovieShopEntities();

        // GET: tblDiscs
        public ActionResult Index()
        {
            var tblDiscs = db.tblDiscs.Include(t => t.tblMovie);
            return View(tblDiscs.ToList());
        }

        // GET: tblDiscs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDisc tblDisc = db.tblDiscs.Find(id);
            if (tblDisc == null)
            {
                return HttpNotFound();
            }
            return View(tblDisc);
        }

        // GET: tblDiscs/Create
        public ActionResult Create()
        {
            ViewBag.movie_id = new SelectList(db.tblMovies, "id", "name");
            return View();
        }

        // POST: tblDiscs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,movie_id,rent_cost,is_Available")] tblDisc tblDisc)
        {
            if (ModelState.IsValid)
            {
                db.tblDiscs.Add(tblDisc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.movie_id = new SelectList(db.tblMovies, "id", "name", tblDisc.movie_id);
            return View(tblDisc);
        }

        // GET: tblDiscs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDisc tblDisc = db.tblDiscs.Find(id);
            if (tblDisc == null)
            {
                return HttpNotFound();
            }
            ViewBag.movie_id = new SelectList(db.tblMovies, "id", "name", tblDisc.movie_id);
            return View(tblDisc);
        }

        // POST: tblDiscs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,movie_id,rent_cost,is_Available")] tblDisc tblDisc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDisc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.movie_id = new SelectList(db.tblMovies, "id", "name", tblDisc.movie_id);
            return View(tblDisc);
        }

        // GET: tblDiscs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDisc tblDisc = db.tblDiscs.Find(id);
            if (tblDisc == null)
            {
                return HttpNotFound();
            }
            return View(tblDisc);
        }

        // POST: tblDiscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDisc tblDisc = db.tblDiscs.Find(id);
            db.tblDiscs.Remove(tblDisc);
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
