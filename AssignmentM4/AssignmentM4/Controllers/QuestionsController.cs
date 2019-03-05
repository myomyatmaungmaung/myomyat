using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentM4;
using Microsoft.AspNet.Identity;

namespace AssignmentM4.Controllers
{
    public class QuestionsController : Controller
    {
        private AssWPEntities db = new AssWPEntities();

        // GET: Questions
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var question = db.Question.Include(q => q.AspNetUsers).Include(q => q.SkinType);
            return View(question.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        [Authorize]
        public ActionResult Create(string qid)
        {
            //ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            //ViewBag.SkinId = new SelectList(db.SkinType, "SkinId", "Name");
            Question question = new Question();
            question.SkinId = qid;
        
            return View(question);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Qid,UserId,SkinId,QusText,Phone")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.UserId = User.Identity.GetUserId();
                db.Question.Add(question);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", "SkinType", new { id = question.SkinId });
            }

            //ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", question.UserId);
            //ViewBag.SkinId = new SelectList(db.SkinType, "SkinId", "Name", question.SkinId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", question.UserId);
            ViewBag.SkinId = new SelectList(db.SkinType, "SkinId", "Name", question.SkinId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Qid,UserId,SkinId,QusText,Phone")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "SkinType", new { id = question.SkinId });

            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", question.UserId);
            ViewBag.SkinId = new SelectList(db.SkinType, "SkinId", "Name", question.SkinId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Question question = db.Question.Find(id);
            string sid = question.SkinId;
            db.Question.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Details", "SkinType", new { id = sid });
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
