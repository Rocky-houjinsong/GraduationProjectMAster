namespace ToRead.RESTAPI.Model
{
    /// <summary>
    /// 评论.
    /// </summary>
    /// <remarks>每个Comment属于一个Post,都有一个AuthorId属性指向作者的ID</remarks>
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}