using ToRead.Library.Models;

namespace ToRead.Library.Services;

public interface IRecommendBlogService
{
    Task<RecommendBlog> GetRecommendBlogAsync();
}