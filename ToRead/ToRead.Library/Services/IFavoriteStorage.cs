using ToRead.Library.Models;

namespace ToRead.Library.Services
{
    public interface IFavoriteStorage
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