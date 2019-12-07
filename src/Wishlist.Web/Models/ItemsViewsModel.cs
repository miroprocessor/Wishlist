using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wishlist.Web.Models
{
    public class ItemViewModel
    {
        [Required]
        public string Name { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name ="Buy Here")]
        public string BuyUrl { get; set; }

        [Display(Name="Is Purchased?")]
        public bool IsPurchased { get; set; }
    }
}