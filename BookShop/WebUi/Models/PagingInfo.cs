using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUi.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; } // all books
        public int ItemsPerPage { get; set; }//count book on page
        public int CurrentPage { get; set; }//number current page
        public int TotalPages//count all pages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}