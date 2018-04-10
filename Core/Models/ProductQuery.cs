using System;
using g4u.Extensions;

namespace g4u.Core.Models
{
    public class ProductQuery : IQueryObject
    {
        public int? PlatformId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsForWishlist { get; set; }
        public string AuthSub { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? AgeLimit { get; set; }
        public string Description { get; set; }
        public string SellOrBuy { get; set; }
        public int? Price { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}