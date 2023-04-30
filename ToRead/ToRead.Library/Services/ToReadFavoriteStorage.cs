using ToRead.Library.Models;

namespace ToRead.Library.Services;

/// <summary>
/// 待读收藏存储类.
/// </summary>
//TODO 收藏功能 实现,重点
public class ToReadFavoriteStorage : IToReadFavoriteStorage
{
    public bool IsInitialized { get; }

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ToReadFavorite> GetFavoriteAsync(int poetryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ToReadFavorite>> GetFavoritesAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveFavoriteAsync(ToReadFavorite favorite)
    {
        throw new NotImplementedException();
    }

    public event EventHandler<FavoriteStorageUpdatedEventArgs> Updated;
}