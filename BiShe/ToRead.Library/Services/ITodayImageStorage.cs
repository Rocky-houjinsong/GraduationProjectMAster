using ToRead.Models;

namespace ToRead.Services;

public interface ITodayImageStorage
{
    Task<TodayImage> GetTodayImageAsync(bool includingImageStream);

    Task SaveTodayImageAsync(TodayImage todayImage, bool savingExpiresAtOnly);
}