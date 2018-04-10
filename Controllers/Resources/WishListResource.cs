namespace g4u.Controllers.Resources
{
    public class WishListResource
    {
        public int Id { get; set; }
		public string AuthSub { get; set; }
        public int ProductId { get; set; }
		public bool ProductIsExist { get; set; }
    }
}