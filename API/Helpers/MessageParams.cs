namespace API.Helpers
{
    public class MessageParams : PaginationParams
    {
        public string Username { get; set; } //current logged in user
        public string Container { get; set; } = "Unread";
    }
}