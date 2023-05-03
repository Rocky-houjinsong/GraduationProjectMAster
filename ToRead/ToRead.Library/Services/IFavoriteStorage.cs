using ToRead.Library.Models;

namespace ToRead.Library.Services
{
    /// <summary>
    /// 待读收藏存储接口
    /// </summary>
    /// <remarks>需要调教</remarks>
    //TODO 对收藏存储 功能模块 进行设计和实现
    public interface IFavoriteStorage
    {
        bool IsInitialized { get; }

        Task InitializeAsync();

        Task<Favorite?> GetFavoriteAsync(int poetryId);

        Task<IEnumerable<Favorite>> GetFavoritesAsync();

        Task SaveFavoriteAsync(Favorite favorite);

        event EventHandler<FavoriteStorageUpdatedEventArgs> Updated;
    }

    public class FavoriteStorageUpdatedEventArgs : EventArgs
    {
        public Favorite UpdatedFavorite { get; }

        public FavoriteStorageUpdatedEventArgs(Favorite favorite)
        {
            UpdatedFavorite = favorite;
        }
    }
}