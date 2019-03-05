using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentM4.Models;

namespace AssignmentM4.Controllers
{
    public class SkinTypeController : Controller
    {
        private AssWPEntities db = new AssWPEntities();

        // GET: SkinType
        public ActionResult Index()
        {
            return View(db.SkinType.ToList());
        }
        public ActionResult Ques(string id)
        {
            return RedirectToAction("Create", "Questions", new { qid = id });
        }
        public ActionResult EditQ(string id)
        {
            return RedirectToAction("Edit", "Questions", new { id = id });
        }
        public ActionResult DeleteQ(string id)
        {
            return RedirectToAction("Delete", "Questions", new { id = id });
        }


        // GET: SkinType/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkinType skinType = db.SkinType.Find(id);
            var question = from m in db.Question
                           select m;
            if (!String.IsNullOrEmpty(id))
            {
                question = question.Where(s => s.SkinId.Equals(id));
            }
            DetailModelView detailModelView = new DetailModelView();
            detailModelView.Question = question.ToList();
            detailModelView.SkinType = skinType;
            if (skinType == null)
            {
                return HttpNotFound();
            }
            
            return View(detailModelView);
        }
        [Authorize(Roles = "Admin")]
        // GET: SkinType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkinType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkinId,Name,Pic,Detail")] SkinType skinType)
        {
            if (ModelState.IsValid)
            {
                db.SkinType.Add(skinType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skinType);
        }

        // GET: SkinType/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkinType skinType = db.SkinType.Find(id);
            if (skinType == null)
            {
                return HttpNotFound();
            }
            return View(skinType);
        }

        // POST: SkinType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkinId,Name,Pic,Detail")] SkinType skinType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skinType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skinType);
        }

        // GET: SkinType/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkinType skinType = db.SkinType.Find(id);
            if (skinType == null)
            {
                return HttpNotFound();
            }
            return View(skinType);
        }

        // POST: SkinType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SkinType skinType = db.SkinType.Find(id);
            db.SkinType.Remove(skinType);
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
