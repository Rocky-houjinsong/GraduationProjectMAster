namespace ToRead.RESTAPI.Model
{
    /// <summary>
    /// 帖子.
    /// </summary>
    /// <remarks>每个Post包含一个或多个Comment</remarks>
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}