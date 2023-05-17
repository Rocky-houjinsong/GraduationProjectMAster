namespace ToRead.RESTAPI.Data;

/// <summary>
/// 每次应用程序启动时自动创建数据库表
/// </summary>
public static class DbInitializer
{
    public static void Initialize(UserContext context)
    {
        context.Database.EnsureCreated();
    }
}