using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;

namespace Gallery.Web.Controllers
{
    public class SubjectController : Controller
    {
        private GalleryDbContext db = new GalleryDbContext();

        // GET: Subject
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["ViewSubjectImages"] = FilterOptions();
            ViewBag.filterChoice = FilterOptions();
            var subjects = db.subjects.Include(s => s.AspNetRole).Include(s => s.AspNetUser).Include(s => s.image);
            return View(subjects.ToList());
        }
        [HttpPost]
        public ActionResult Index(int? filterChoice)
        {
            ViewData["ViewSubjectImages"] = FilterOptions();
            ViewBag.filterChoice = FilterOptions();
            var subjects = db.subjects.Include(s => s.AspNetRole).Include(s => s.AspNetUser).Include(s => s.image);
            if (filterChoice == 01)
            {
                return View(subjects.Where(x => x.galleries.Count > 0).ToList());
            }
            if (filterChoice == 02)
            {
                return View(subjects.Where(x => x.galleries.Count == 0).ToList());
            }
            if (filterChoice == 03)
            {
                return View(subjects.Where(x => x.images.Count > 0).ToList());
            }
            if (filterChoice == 04)
            {
               return View(subjects.Where(x => x.images.Count == 0).ToList());
            }
            else
            {
                return View(subjects.ToList());
            }
        }
        // GET: Subject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            ViewBag.PermissionLevel = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.AddedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ProfilePicId = new SelectList(db.images, "ImageID", "Filename");
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectID,Name,DateOfBirth,Country,City,Nationality,DateCreated,AddedBy,Reported,ProfilePicId,PermissionLevel,AgeRestricted,IsHidden")] subject subject)
        {
            if (ModelState.IsValid)
            {
                db.subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PermissionLevel = new SelectList(db.AspNetRoles, "Id", "Name", subject.PermissionLevel);
            ViewBag.AddedBy = new SelectList(db.AspNetUsers, "Id", "Email", subject.AddedBy);
            ViewBag.ProfilePicId = new SelectList(db.images, "ImageID", "Filename", subject.ProfilePicId);
            return View(subject);
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.PermissionLevel = new SelectList(db.AspNetRoles, "Id", "Name", subject.PermissionLevel);
            ViewBag.AddedBy = new SelectList(db.AspNetUsers, "Id", "Email", subject.AddedBy);
            ViewBag.ProfilePicId = new SelectList(db.images, "ImageID", "Filename", subject.ProfilePicId);
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectID,Name,DateOfBirth,Country,City,Nationality,DateCreated,AddedBy,Reported,ProfilePicId,PermissionLevel,AgeRestricted,IsHidden")] subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PermissionLevel = new SelectList(db.AspNetRoles, "Id", "Name", subject.PermissionLevel);
            ViewBag.AddedBy = new SelectList(db.AspNetUsers, "Id", "Email", subject.AddedBy);
            ViewBag.ProfilePicId = new SelectList(db.images, "ImageID", "Filename", subject.ProfilePicId);
            return View(subject);
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subject subject = db.subjects.Find(id);
            db.subjects.Remove(subject);
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

        private SelectList FilterOptions()
        {
            var filterList = new SelectList(new Dictionary<string, int> 
        {
            {"Default", 00},
            {"Subjects with galleries", 01},
            {"Subjects without galleries", 02},
            {"Subjects with images", 03},
            {"Subjects without images", 04}
        },
        "Value", "Key");
            return filterList;
        }

    }
}
