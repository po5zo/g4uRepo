namespace g4u.Core.Models
{
    public class AuthSettings
    {
        public string ClientID { get; set; }
        public string Domain { get; set; }
        public string ResponseType { get; set; }
        public string Audience { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
    }
}