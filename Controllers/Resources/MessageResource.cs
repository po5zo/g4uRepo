namespace g4u.Controllers.Resources
{
    public class MessageResource
    {
        public int Id { get; set; }
        
        public string Content { get; set; }
        
        public bool? Seen { get; set; }
    }
}