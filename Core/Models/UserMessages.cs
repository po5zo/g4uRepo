using System.ComponentModel.DataAnnotations;

namespace g4u.Core.Models
{
    public class UserMessages
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsSender { get; set; }

        [Required]
        public Message Message { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        public User User { get; set; }
        
        [Required]
        public int UserId { get; set; }      
    }
}