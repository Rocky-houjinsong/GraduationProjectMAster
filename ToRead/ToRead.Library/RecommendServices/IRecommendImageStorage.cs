using ToRead.Library.Models;

public interface IRecommendImageStorage
{
    Task<RecommendImage> GetTodayImageAsync(bool includingImageStream);

    Task SaveTodayImageAsync(RecommendImage todayImage, bool savingExpiresAtOnly);
}