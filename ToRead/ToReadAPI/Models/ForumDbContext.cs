using Microsoft.EntityFrameworkCore;

namespace ToReadAPI.Models;

/*添加新建项”对话框中，选择“数据”类别，并选择“类”，然后单击“添加”按钮。
 继承自 EntityFrameworkCore 中的 DbContext 类
更新 appsettings.json 文件，以便它包含一个连接字符串，该连接字符串指向数据库*/
/*ForumDbContext 继承自 Entity Framework Core 的 DbContext 类，它包含一个构造函数和三个 DbSet 属性，分别表示 User、Post 和 Comment 数据库表。每个实体类都包含一些属性，用于将其映射到数据库表的列*/
public class ForumDbContext : DbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// 声明一个 DbSet 属性，以表示 Forum 实体集合
    /// </summary>
    public DbSet<Forum> Forums { get; set; }

    // 定义论坛相关的数据库表
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    // 论坛用户
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

    // 论坛帖子
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    // 帖子评论
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; }
        public Post Post { get; set; }
    }
}