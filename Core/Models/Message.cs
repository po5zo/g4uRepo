using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace g4u.Core.Models
{
    public class Message
    {
        [Display(Name = "Message ID")]
        [Required]
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        
        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; }
         
        [Display(Name = "Seen")]
        [Required]
        public bool? Seen { get; set; }
    }
}
