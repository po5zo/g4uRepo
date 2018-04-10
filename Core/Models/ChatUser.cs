using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace g4u.Core.Models
{
    public class ChatUser
    {
        [Required]
        [Key]
        public int Id { get; set; }

		[Display(Name = "Nickname")]
		[Required]
        [StringLength(255)]
		public string Nickname {get;set;}
		
        [Display(Name = "Email")]
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
