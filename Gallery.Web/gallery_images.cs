//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gallery.Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class gallery_images
    {
        public int ImageID { get; set; }
        public int GalleryID { get; set; }
        public System.DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public long TimesViewed { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual gallery gallery { get; set; }
        public virtual image image { get; set; }
    }
}
