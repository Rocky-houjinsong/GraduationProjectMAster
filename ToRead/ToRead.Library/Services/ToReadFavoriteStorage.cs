using SQLite;
using ToRead.Library.Models;

namespace ToRead.Library.Services;

/// <summary>
/// 待读收藏存储类.
/// </summary>
//TODO 收藏功能 实现,重点
public class ToReadFavoriteStorage : IToReadFavoriteStorage
{
    public const string DbName = "favoritedb.sqlite3";

    public static readonly string PoetryDbPath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData), DbName);

    public Task SaveFavoriteAsync(ToReadFavorite favorite)
    {
        throw new NotImplementedException();
    }

    public event EventHandler<FavoriteStorageUpdatedEventArgs>? Updated;

    private SQLiteAsyncConnection? _connection;

    private SQLiteAsyncConnection Connection =>
        _connection ??= new SQLiteAsyncConnection(PoetryDbPath);

    private readonly IPreferenceStorage _preferenceStorage;

    public ToReadFavoriteStorage(IPreferenceStorage preferenceStorage)
    {
        _preferenceStorage = preferenceStorage;
    }

    public bool IsInitialized =>
        _preferenceStorage.Get(FavoriteStorageConstant.VersionKey,
            default(int)) == FavoriteStorageConstant.Version;

    public async Task InitializeAsync()
    {
        await Connection.CreateTableAsync<ToReadFavorite>();
        _preferenceStorage.Set(FavoriteStorageConstant.VersionKey,
            FavoriteStorageConstant.Version);
    }

    Task<ToReadFavorite> IToReadFavoriteStorage.GetFavoriteAsync(int poetryId)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<ToReadFavorite>> IToReadFavoriteStorage.GetFavoritesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ToReadFavorite?> GetFavoriteAsync(int poetryId) =>
        await Connection.Table<ToReadFavorite>()
            .FirstOrDefaultAsync(p => p.PoetryId == poetryId);

    public async Task<IEnumerable<ToReadFavorite>> GetFavoritesAsync() =>
        await Connection.Table<ToReadFavorite>().Where(p => p.IsFavorite)
            .OrderByDescending(p => p.Timestamp).ToListAsync();

    public async Task SaveFavoriteAsync(ToReadFavorite favorite)
    {
        favorite.Timestamp = DateTime.Now.Ticks;
        await Connection.InsertOrReplaceAsync(favorite);
        Updated?.Invoke(this, new FavoriteStorageUpdatedEventArgs(favorite));
    }

    public async Task CloseAsync() => await Connection.CloseAsync();
}

public static class FavoriteStorageConstant
{
    public const string VersionKey =
        nameof(FavoriteStorageConstant) + "." + nameof(Version);

    public const int Version = 1;
}