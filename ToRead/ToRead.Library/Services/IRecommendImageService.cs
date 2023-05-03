using ToRead.Library.Models;
using ToRead.Library.RecommendServices;

namespace ToRead.Library.Services;

public interface IRecommendImageService
{
    Task<RecommendImage> GetTodayImageAsync();

    Task<TodayImageServiceCheckUpdateResult> CheckUpdateAsync();
}

public class RecommendImageServiceCheckUpdateResult
{
    public bool HasUpdate { get; set; }

    public RecommendImage TodayImage { get; set; } = new();
}