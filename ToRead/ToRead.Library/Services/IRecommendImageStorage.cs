using ToRead.Library.Models;

namespace ToRead.Library.Services;

public interface IRecommendImageStorage
{
    Task<RecommendImage> GetTodayImageAsync(bool includingImageStream);

    Task SaveTodayImageAsync(RecommendImage todayImage, bool savingExpiresAtOnly);
}