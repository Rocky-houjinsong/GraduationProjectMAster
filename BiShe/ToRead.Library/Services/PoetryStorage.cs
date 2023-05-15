using System.Linq.Expressions;
using ToRead.Models;
using SQLite;

namespace ToRead.Services;

public class PoetryStorage : IPoetryStorage
{
    public const int NumberPoetry = 30;

    public const string DbName = "poetrydb.sqlite3";

    public static readonly string PoetryDbPath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData), DbName);

    private SQLiteAsyncConnection? _connection;

    private SQLiteAsyncConnection Connection =>
        _connection ??= new SQLiteAsyncConnection(PoetryDbPath);

    private readonly IPreferenceStorage _preferenceStorage;

    public PoetryStorage(IPreferenceStorage preferenceStorage)
    {
        _preferenceStorage = preferenceStorage;
    }

    public bool IsInitialized =>
        _preferenceStorage.Get(PoetryStorageConstant.VersionKey,
            default(int)) == PoetryStorageConstant.Version;

    public async Task InitializeAsync()
    {
        await using var dbFileStream =
            new FileStream(PoetryDbPath, FileMode.OpenOrCreate);
        await using var dbAssetStream =
            typeof(PoetryStorage).Assembly.GetManifestResourceStream(DbName) ??
            throw new Exception($"Manifest not found: {DbName}");
        await dbAssetStream.CopyToAsync(dbFileStream);

        _preferenceStorage.Set(PoetryStorageConstant.VersionKey,
            PoetryStorageConstant.Version);
    }

    public Task<Poetry> GetPoetryAsync(int id) =>
        Connection.Table<Poetry>().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Poetry>> GetPoetriesAsync(
        Expression<Func<Poetry, bool>> where, int skip, int take) =>
        await Connection.Table<Poetry>().Where(where).Skip(skip).Take(take)
            .ToListAsync();

    //TODO 实现 从 云端添加的方法
    public async Task AddPoetryAsync(TodayPoetry poetry)
    {
        var poetrylast = await Connection.Table<Poetry>().OrderByDescending(p => p.Id).FirstOrDefaultAsync();
        int lastId = poetrylast.Id + 1;

        var poetryinsert = new Poetry
        {
            Id = lastId,
            Name = poetry.Name,
            Author = poetry.Author,
            Dynasty = poetry.Dynasty,
            Content = poetry.Content
        };
        await Connection.InsertAllAsync(new[] { poetryinsert });
    }


    public async Task CloseAsync() => await Connection.CloseAsync();
}

public static class PoetryStorageConstant
{
    public const string VersionKey =
        nameof(PoetryStorageConstant) + "." + nameof(Version);

    public const int Version = 1;
}