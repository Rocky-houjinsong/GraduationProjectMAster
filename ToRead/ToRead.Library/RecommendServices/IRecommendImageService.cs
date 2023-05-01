using ToRead.Library.Models;

namespace ToRead.Library.RecommendServices;

/// <summary>
/// 今日推荐 图片服务接口
/// </summary>
//TODO 修改对象,简单
public interface IRecommendImageService
{
    Task<RecommendImage> GetTodayImageAsync();

    Task<TodayImageServiceCheckUpdateResult> CheckUpdateAsync();
}

public class TodayImageServiceCheckUpdateResult
{
    public bool HasUpdate { get; set; }

    public RecommendImage TodayImage { get; set; } = new();
}