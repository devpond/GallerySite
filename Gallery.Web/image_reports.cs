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
    
    public partial class image_reports
    {
        public int ImageReportID { get; set; }
        public int ImageID { get; set; }
        public string ReportedBy { get; set; }
        public int Type { get; set; }
        public System.DateTime ReportDate { get; set; }
        public string Comments { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual image image { get; set; }
        public virtual report_types report_types { get; set; }
    }
}
