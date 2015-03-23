using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.Provider;

namespace Gallery.Web.Controllers
{
    public class ImagesController : Controller
    {
        private GalleryDbContext db = new GalleryDbContext();
        // GET: Images
        public ActionResult Index()
        {
            return View(db.images.ToList());
        }

        [HttpGet]
        public ActionResult ShowImage()
        {
            ViewBag.ApplicationTitle = "ViewImages";

            var obj = GetNames();
            
            ViewBag.SubjectList = obj;

            return View(db.ViewSubjectImages.ToList());
        }
        [HttpPost]
        public ActionResult ShowImage(int subjectList)
        {
            //var subjects = from m in db.ViewSubjectImages
            //    select m;
            
            ViewBag.SubjectList = GetNames();
            var imgList = new List<ViewSubjectImage>();
            ViewData["ViewSubjectImages"] = new SelectList(imgList, "SubjectID", "Name");
            
            foreach (var i in db.ViewSubjectImages)
            {
                if (i.SubjectID.Equals(subjectList))
                {
                    imgList.Add(i);
                }

            }
            return View(imgList);
        }

        private List<SelectListItem> GetNames()
        {
            var obj = new List<SelectListItem>();
            foreach (var i in db.ViewSubjectNamesAndIds.SortBy("Name"))
            {
                    obj.Add(new SelectListItem()
                    {
                        Text = i.Name,
                        Value = i.SubjectID.ToString(CultureInfo.InvariantCulture)
                    });
            }
            return obj;
        }

        public ActionResult RandomImage()
        {

            var selection = db.images.Where(x => x.IsHidden == 0);
            image image = selection.OrderBy(x => x.ImageID)
                .Skip(new Random().Next(selection.Count()))
                .First();
            return PartialView(image);
        }

        public ActionResult ViewImage(int? Id)
        {
            var selImage = db.images.SingleOrDefault(x => x.ImageID == Id);
            return View(selImage);
        }
    }
}