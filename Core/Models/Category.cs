using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace g4u.Core.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }
		
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
