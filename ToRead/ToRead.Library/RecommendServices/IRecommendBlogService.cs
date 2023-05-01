using ToRead.Library.Models;

namespace ToRead.Library.RecommendServices;

public interface IRecommendBlogService
{
    Task<RecommendBlog> GetTodayPoetryAsync();
}

public static class TodayPoetrySources
{
    public const string Jinrishici = nameof(Jinrishici);

    public const string Local = nameof(Local);
}