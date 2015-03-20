﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Gallery.Web.Controllers
{
    public class GalleryController : Controller
    {
        private GalleryDbContext db = new GalleryDbContext();
        // GET: Gallery
        public ActionResult Index()
        {
            return View(db.ViewAllGalleries.ToList());
        }

        public ActionResult ViewGallery(int? Id)
        {
            var galRating = db.ViewGalleryRatings.Where(x => x.GalleryID == Id)
                .Select(x => x.AverageRating)
                .FirstOrDefault();
            var galRating2 = 0.0;
            if (galRating != null)
            {
                galRating2 = Math.Truncate((double) (galRating*100))/100;
            }

            ViewBag.galleryName = db.ViewGalleryImages.Where(x => x.GalleryID == Id)
                .Select(x => x.GalleryName).FirstOrDefault();
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

            
            var selectedGallery = db.ViewGalleryImages.Where(x => x.GalleryID == Id);
            return View(selectedGallery);
        }
        [HttpGet]
        public ActionResult GalleryVoteControl()
        {
            var glv = new gallery_votes();
            
            //ViewBag.voteList = voteList;
            return PartialView("GalleryVoteControl", glv);
        }

        [HttpPost]
        public ActionResult GalleryVoteControl(gallery_votes glv)
        {
            if (ModelState.IsValid)
            {
                //db.SaveChanges();
                return PartialView("GalleryVoteControl", glv);
            }
            else
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult GalleryRateControl()
        {
            var glv = new gallery_ratings();
            var rateList = new List<int>()
            {
                {01}, {02}, {03}, {04}, {05}, {06}, {07}, {08}, {09}, {10}
            };
            ViewBag.rateList = rateList;
            return PartialView("_GalleryRateControl", glv);
        }

        [HttpPost]
        public ActionResult GalleryRateControl(gallery_ratings glr)
        {
            if (ModelState.IsValid)
            {
                //db.SaveChanges();
                return PartialView("_GalleryRateControl", glr);
            }
            else
            {
                return View("Error");
            }
        }
    }
}