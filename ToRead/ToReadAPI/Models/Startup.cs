using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ToReadAPI.Models;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //创建了 ForumDbContext，并将其注册到了依赖注入容器中。
        //要在控制器中使用该上下文对象，通过构造函数注入它
        services.AddDbContext<ForumDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("ForumDatabase")));
    }
}