using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g4u.Controllers.Resources
{
    public class SaveWishlistResource
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
		public string AuthSub { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
		public bool ProductIsExist { get; set; }
    }
}