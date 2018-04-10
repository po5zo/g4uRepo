using g4u.Extensions;

namespace g4u.Core.Models
{
    public class UserQuery : IQueryObject
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public string AuthSub { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}