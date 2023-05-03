using static ToReadAPI.Models.ForumDbContext;

namespace ToReadAPI.Models;

public class Forum
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Topic> Topics { get; set; }

    public Forum()
    {
        Topics = new List<Topic>();
    }

    public void AddTopic(Topic topic)
    {
        Topics.Add(topic);
    }
}

public class Topic
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public User Author { get; set; }
    public ICollection<Post> Posts { get; set; }

    public Topic()
    {
        Posts = new List<Post>();
    }

    public void AddPost(Post post)
    {
        Posts.Add(post);
    }
}
public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public User Author { get; set; }
    public Topic Topic { get; set; }
}