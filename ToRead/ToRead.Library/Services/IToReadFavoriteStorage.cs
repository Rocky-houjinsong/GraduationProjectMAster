using ToRead.Library.Models;

namespace ToRead.Library.Services
{
    /// <summary>
    /// 待读收藏存储接口
    /// </summary>
    /// <remarks>需要调教</remarks>
    //TODO 对收藏存储 功能模块 进行设计和实现
    public interface IToReadFavoriteStorage
    {
        bool IsInitialized { get; }

        Task InitializeAsync();

        Task<ToReadFavorite?> GetFavoriteAsync(int poetryId);

        Task<IEnumerable<ToReadFavorite>> GetFavoritesAsync();

        Task SaveFavoriteAsync(ToReadFavorite favorite);

        event EventHandler<FavoriteStorageUpdatedEventArgs> Updated;
    }

    public class FavoriteStorageUpdatedEventArgs : EventArgs
    {
        public ToReadFavorite UpdatedFavorite { get; }

        public FavoriteStorageUpdatedEventArgs(ToReadFavorite favorite)
        {
            UpdatedFavorite = favorite;
        }
    }
}