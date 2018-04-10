using g4u.Extensions;

namespace g4u.Core.Models
{
    public class WishlistQuery : IQueryObject
    {
        public string AuthSub { get; set; }
        public int? ProductId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}