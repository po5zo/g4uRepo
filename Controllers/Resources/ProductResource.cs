using System;

namespace g4u.Controllers.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? AgeLimit { get; set; }

        public int Price { get; set; }

        public DateTime? ReleaseDate { get; set; }  

        public string SellOrBuy { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int PlatformId { get; set; }
        public string AuthSub { get; set; }
    }
}