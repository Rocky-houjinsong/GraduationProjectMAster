using ToRead.RESTAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToRead.RESTAPI.Services;

/// <summary>
/// 继承了 IdentityDbContext&lt;User&gt; 类，表示使用 ASP.NET Core Identity 进行认证权
/// </summary>
public class ApplicationDbContext : IdentityDbContext<User>
{
    //添加了两个 DbSet<Post> 和 DbSet<Comment> 属性，
    //表示在数据库中创建 Post 和 Comment 表。
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}