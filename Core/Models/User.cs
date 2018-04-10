using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace g4u.Core.Models
{
    public class User
    {
        [Display(Name = "User ID")]
        [Required]
        [Key]
        public int Id { get; set; }
        public string AuthSub { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get { return FamilyName + " " + GivenName; } }
        public string Locale { get; set; }
        public bool Email_Verified { get; set; }
        public string Roles { get; set; }
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }
        [Display(Name = "Mobil")]
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public byte[] Image { get; set; }
        public string DefaultImageLink { get; set; }
        public string Gender { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<History> Histories { get; set; }
        public ICollection<Wishlist> WishList { get; set; }
        public User()
        {
            Products = new Collection<Product>();
            Histories = new Collection<History>();
            WishList = new Collection<Wishlist>();
        }
    }
}
