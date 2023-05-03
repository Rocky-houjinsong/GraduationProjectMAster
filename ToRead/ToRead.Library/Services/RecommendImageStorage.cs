using ToRead.Library.Models;

namespace ToRead.Library.Services;

public class RecommendImageStorage : IRecommendImageStorage
{
    private readonly IPreferenceStorage _preferenceStorage;

    public RecommendImageStorage(IPreferenceStorage preferenceStorage)
    {
        _preferenceStorage = preferenceStorage;
    }

    public static readonly string FullStartDateKey = nameof(RecommendImageStorage) +
                                                     "." + nameof(RecommendImage.FullStartDate);

    public static readonly string ExpiresAtKey =
        nameof(RecommendImageStorage) + "." + nameof(RecommendImage.ExpiresAt);

    public static readonly string CopyrightKey =
        nameof(RecommendImageStorage) + "." + nameof(RecommendImage.Copyright);

    public static readonly string CopyrightLinkKey = nameof(RecommendImageStorage) +
                                                     "." + nameof(RecommendImage.CopyrightLink);

    public const string FullStartDateDefault = "201901010700";

    public static readonly DateTime ExpiresAtDefault = new(2019, 1, 2, 7, 0, 0);

    public const string CopyrightDefault =
        "Salt field province vietnam work (© Quangpraha/Pixabay)";

    public const string CopyrightLinkDefault =
        "https://pixabay.com/photos/salt-field-province-vietnam-work-3344508/";

    public const string FileName = "todayImage.bin";

    public static readonly string TodayImagePath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData), FileName);

    public async Task<RecommendImage>
        GetTodayImageAsync(bool includingImageStream)
    {
        var todayImage = new RecommendImage
        {
            FullStartDate =
                _preferenceStorage.Get(FullStartDateKey, FullStartDateDefault),
            ExpiresAt = _preferenceStorage.Get(ExpiresAtKey, ExpiresAtDefault),
            Copyright = _preferenceStorage.Get(CopyrightKey, CopyrightDefault),
            CopyrightLink = _preferenceStorage.Get(CopyrightLinkKey,
                CopyrightLinkDefault)
        };

        if (!File.Exists(TodayImagePath))
        {
            await using var imageAssetFileStream =
                new FileStream(TodayImagePath, FileMode.Create) ??
                throw new NullReferenceException("Null file stream.");
            await using var imageAssetStream =
                typeof(RecommendImageStorage).Assembly.GetManifestResourceStream(
                    FileName) ??
                throw new NullReferenceException(
                    "Null manifest resource stream");
            await imageAssetStream.CopyToAsync(imageAssetFileStream);
        }

        if (!includingImageStream)
        {
            return todayImage;
        }

        var imageMemoryStream = new MemoryStream();
        await using var imageFileStream =
            new FileStream(TodayImagePath, FileMode.Open);
        await imageFileStream.CopyToAsync(imageMemoryStream);
        todayImage.ImageBytes = imageMemoryStream.ToArray();

        return todayImage;
    }

    public async Task SaveTodayImageAsync(RecommendImage todayImage,
        bool savingExpiresAtOnly)
    {
        _preferenceStorage.Set(ExpiresAtKey, todayImage.ExpiresAt);
        if (savingExpiresAtOnly)
        {
            return;
        }

        if (todayImage.ImageBytes == null)
        {
            throw new ArgumentException($"Null image bytes.",
                nameof(todayImage));
        }

        _preferenceStorage.Set(FullStartDateKey, todayImage.FullStartDate);
        _preferenceStorage.Set(CopyrightKey, todayImage.Copyright);
        _preferenceStorage.Set(CopyrightLinkKey, todayImage.CopyrightLink);

        await using var imageFileStream =
            new FileStream(TodayImagePath, FileMode.Create);
        await imageFileStream.WriteAsync(todayImage.ImageBytes, 0,
            todayImage.ImageBytes.Length);
    }
}