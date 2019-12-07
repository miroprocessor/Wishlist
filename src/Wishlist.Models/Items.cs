using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wishlist.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string BuyUrl { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Is Purchased?")]
        public bool IsPurchased { get; set; }

    }
}
