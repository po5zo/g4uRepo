using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace g4u.Core.Models
{
    public class Wishlist
    {
        [Required]
        [Key]
        public int Id { get; set; }
		
        [Required]
		public bool ProductIsExist { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string AuthSub { get; set; }
    }
}
