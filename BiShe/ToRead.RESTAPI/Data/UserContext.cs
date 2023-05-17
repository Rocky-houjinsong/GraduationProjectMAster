using Microsoft.EntityFrameworkCore;
using ToRead.RESTAPI.Models;

namespace ToRead.RESTAPI.Data;

public class UserContext : DbContext
{
    /// <summary>
    /// 属性, 针对User表的设置
    /// </summary>
    public DbSet<User> Users { get; set; }
    /// <summary>
    /// 以配置数据库链接字符串
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("")
    }

}