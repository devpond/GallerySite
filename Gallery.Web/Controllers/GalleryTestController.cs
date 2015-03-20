using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.DynamicData;
using System.Web.ModelBinding;

namespace Gallery.Web.Controllers
{
    public class GalleryTestController : Controller
    {
        // GET: GalleryTest
        private GalleryDbContext db = new GalleryDbContext();
        // GET: Gallery
        public ActionResult Index()
        {
            var galList = db.galleries.ToList();
            return View(galList);
        }
        [HttpGet]
        public ActionResult ViewGallery(int? Id)
        {
            gallery selectedGallery = db.galleries.Find(Id);
            var galRating = db.ViewGalleryRatings.Where(x => x.GalleryID == Id)
                .Select(x => x.AverageRating)
                .FirstOrDefault();
            var galRating2 = 0.0;
            if (galRating != null)
            {
                galRating2 = Math.Truncate((double)(galRating * 100)) / 100;
            }

            var rateList = new List<int>()
            {
                {01}, {02}, {03}, {04}, {05}, {06}, {07}, {08}, {09}, {10}
            };
            ViewBag.rateList = rateList;

            if (galRating2 != null)
            {
                ViewBag.galleryRating = galRating2;
            }
            else
            {
                ViewBag.galleryRating = 0;
            }
            ViewBag.galleryUpVotes = db.ViewGalleryVotes
                .Where(x => x.GalleryID == Id)
                .Where(x => x.VoteType == "Up")
                .Select(x => x.TotalVotes).FirstOrDefault();
            ViewBag.galleryDownVotes = db.ViewGalleryVotes
                .Where(x => x.GalleryID == Id)
                .Where(x => x.VoteType == "Down")
                .Select(x => x.TotalVotes).FirstOrDefault();
            ViewBag.galleryId = Id;

            return View(selectedGallery);
        }

        [HttpPost]
        public ActionResult ViewGallery(gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            else
            {
                return View("Error");
            }
        }
    }
}