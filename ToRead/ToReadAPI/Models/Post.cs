namespace ToReadAPI.Models;

/// <summary>
/// 帖子
/// </summary>
/// <remarks>一个标题、内容、作者以及所属的论坛等信息</remarks>
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public Forum Forum { get; set; }
    public int ForumId { get; set; }
}