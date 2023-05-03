using ToRead.Library.Models;
using ToRead.Library.Services;

namespace ToRead.Library.RecommendServices;

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

    public const string FileName = "RecommendImage.bin";

    public static readonly string RecommendImagePath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData), FileName);

    public async Task<RecommendImage>
        GetRecommendImageAsync(bool includingImageStream)
    {
        var RecommendImage = new RecommendImage
        {
            FullStartDate =
                _preferenceStorage.Get(FullStartDateKey, FullStartDateDefault),
            ExpiresAt = _preferenceStorage.Get(ExpiresAtKey, ExpiresAtDefault),
            Copyright = _preferenceStorage.Get(CopyrightKey, CopyrightDefault),
            CopyrightLink = _preferenceStorage.Get(CopyrightLinkKey,
                CopyrightLinkDefault)
        };

        if (!File.Exists(RecommendImagePath))
        {
            await using var imageAssetFileStream =
                new FileStream(RecommendImagePath, FileMode.Create) ??
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
            return RecommendImage;
        }

        var imageMemoryStream = new MemoryStream();
        await using var imageFileStream =
            new FileStream(RecommendImagePath, FileMode.Open);
        await imageFileStream.CopyToAsync(imageMemoryStream);
        RecommendImage.ImageBytes = imageMemoryStream.ToArray();

        return RecommendImage;
    }

    public async Task SaveRecommendImageAsync(RecommendImage RecommendImage,
        bool savingExpiresAtOnly)
    {
        _preferenceStorage.Set(ExpiresAtKey, RecommendImage.ExpiresAt);
        if (savingExpiresAtOnly)
        {
            return;
        }

        if (RecommendImage.ImageBytes == null)
        {
            throw new ArgumentException($"Null image bytes.",
                nameof(RecommendImage));
        }

        _preferenceStorage.Set(FullStartDateKey, RecommendImage.FullStartDate);
        _preferenceStorage.Set(CopyrightKey, RecommendImage.Copyright);
        _preferenceStorage.Set(CopyrightLinkKey, RecommendImage.CopyrightLink);

        await using var imageFileStream =
            new FileStream(RecommendImagePath, FileMode.Create);
        await imageFileStream.WriteAsync(RecommendImage.ImageBytes, 0,
            RecommendImage.ImageBytes.Length);
    }

    public Task<RecommendImage> GetTodayImageAsync(bool includingImageStream)
    {
        throw new NotImplementedException();
    }

    public Task SaveTodayImageAsync(RecommendImage todayImage, bool savingExpiresAtOnly)
    {
        throw new NotImplementedException();
    }
}