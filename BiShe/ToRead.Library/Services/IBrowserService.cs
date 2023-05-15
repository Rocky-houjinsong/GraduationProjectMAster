namespace ToRead.Services;

/// <summary>
/// IBrowserService 接口的作用是封装了调用系统默认浏览器打开指定 URL 的功能。
/// 通过在应用程序中依赖注入 IBrowserService 接口，
/// 其他组件或服务就可以使用该接口提供的方法来打开指定的 URL，
/// 而不需要直接调用系统 API 或硬编码相关实现逻辑。这样可以带来更好的解耦和可测试性
/// </summary>
public interface IBrowserService
{
    Task OpenAsync(string url);
}