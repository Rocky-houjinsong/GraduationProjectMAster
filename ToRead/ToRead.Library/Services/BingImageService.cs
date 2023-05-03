using System.Globalization;
using System.Text.Json;
using ToRead.Library.Misc;
using ToRead.Library.Models;
using ToRead.Library.RecommendServices;

namespace ToRead.Library.Services;

public class BingImageService : IRecommendImageService
{
    private IRecommendImageStorage _todayImageStorage;

    private IAlertService _alertService;

    private const string Server = "必应每日图片服务器";

    public BingImageService(IRecommendImageStorage todayImageStorage,
        IAlertService alertService)
    {
        _todayImageStorage = todayImageStorage;
        _alertService = alertService;
    }

    public async Task<RecommendImage> GetTodayImageAsync() =>
        await _todayImageStorage.GetTodayImageAsync(true);

    public async Task<TodayImageServiceCheckUpdateResult> CheckUpdateAsync()
    {
        var todayImage = await _todayImageStorage.GetTodayImageAsync(false);
        if (todayImage.ExpiresAt > DateTime.Now)
        {
            return new TodayImageServiceCheckUpdateResult { HasUpdate = false };
        }

        using var httpClient = new HttpClient();
        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync(
                "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.HttpClientErrorTitle,
                ErrorMessages.GetHttpClientError(Server, e.Message),
                ErrorMessages.HttpClientErrorButton);
            return new TodayImageServiceCheckUpdateResult { HasUpdate = false };
        }

        var json = await response.Content.ReadAsStringAsync();
        string bingImageUrl;
        try
        {
            var bingImage = JsonSerializer.Deserialize<BingImageOfTheDay>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })
                ?.Images?.FirstOrDefault() ?? throw new JsonException();

            var bingImageFullStartDate = DateTime.ParseExact(
                bingImage.FullStartDate ?? throw new JsonException(),
                "yyyyMMddHHmm", CultureInfo.InvariantCulture);
            var todayImageFullStartDate = DateTime.ParseExact(
                todayImage.FullStartDate, "yyyyMMddHHmm",
                CultureInfo.InvariantCulture);

            if (bingImageFullStartDate <= todayImageFullStartDate)
            {
                todayImage.ExpiresAt = DateTime.Now.AddHours(2);
                await _todayImageStorage.SaveTodayImageAsync(todayImage, true);
                return new TodayImageServiceCheckUpdateResult
                {
                    HasUpdate = false
                };
            }

            todayImage = new RecommendImage
            {
                FullStartDate = bingImage.FullStartDate,
                ExpiresAt = bingImageFullStartDate.AddDays(1),
                Copyright = bingImage.Copyright ?? throw new JsonException(),
                CopyrightLink = bingImage.CopyrightLink ??
                                throw new JsonException()
            };

            bingImageUrl = bingImage.Url ?? throw new JsonException();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.JsonDeserializationErrorTitle,
                ErrorMessages.GetJsonDeserializationError(Server, e.Message),
                ErrorMessages.JsonDeserializationErrorButton);
            return new TodayImageServiceCheckUpdateResult { HasUpdate = false };
        }

        try
        {
            response =
                await httpClient.GetAsync("https://www.bing.com" +
                                          bingImageUrl);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            _alertService.Alert(ErrorMessages.HttpClientErrorTitle,
                ErrorMessages.GetHttpClientError(Server, e.Message),
                ErrorMessages.HttpClientErrorButton);
            return new TodayImageServiceCheckUpdateResult { HasUpdate = false };
        }

        todayImage.ImageBytes = await response.Content.ReadAsByteArrayAsync();
        await _todayImageStorage.SaveTodayImageAsync(todayImage, false);
        return new TodayImageServiceCheckUpdateResult
        {
            HasUpdate = true,
            TodayImage = todayImage
        };
    }
}

public class BingImageOfTheDay
{
    public List<BingImageOfTheDayImage>? Images { get; set; }
}

public class BingImageOfTheDayImage
{
    public string? StartDate { get; set; }

    public string? FullStartDate { get; set; }

    public string? EndDate { get; set; }

    public string? Url { get; set; }

    public string? Copyright { get; set; }

    public string? CopyrightLink { get; set; }
}