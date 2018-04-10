namespace g4u.Controllers.Resources
{
    public class WishlistQueryResource
    {
        public string AuthSub { get; set; }
        public int? ProductId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}