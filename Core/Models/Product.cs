using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g4u.Core.Models
{
    public class Product
    {
        [Display(Name = "Product ID")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Age limit")]
        [Required]
        public int? AgeLimit { get; set; }

        [Display(Name = "Price")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "Release date")]
        public DateTime? ReleaseDate { get; set; }   

        [Display(Name = "Sell/Buy")]
        public string SellOrBuy { get; set; }

        [Display(Name = "Created date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Modified date")]
        public DateTime? LastUpdate { get; set; }

        [Display(Name = "Delete Date")]
        public DateTime? DeleteDate { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Platform ID")]
		[Required]
        public int PlatformId { get; set; }
        
        [Required]
        public Platform Platform { get; set; }      

        [Required]
        public Category Category { get; set; }

        [Display(Name = "Category ID")]
		[Required]
        public int CategoryId { get; set; }
        
        [Required]
        public string AuthSub { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public Product()
        {
            Photos = new Collection<Photo>();
        }
    }
}
