using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using g4u.Core.Models;

namespace g4u.Controllers.Resources
{
    public class UserResource
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        public string AuthSub { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get { return FamilyName + " " + GivenName; } }
        public string locale { get; set; }
        public bool Email_Verified { get; set; }
        public string roles { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public byte[] Image { get; set; }
        public string DefaultImageLink { get; set; }
        public string Gender { get; set; }

        public int? Rating { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<History> Histories { get; set; }
        public ICollection<Wishlist> WishList { get; set; }
        public UserResource()
        {
            Products = new Collection<Product>();
            Histories = new Collection<History>();
            WishList = new Collection<Wishlist>();
        }
    }
}