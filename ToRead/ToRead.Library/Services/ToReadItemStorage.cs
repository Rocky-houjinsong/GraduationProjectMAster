using SQLite;
using System.Linq.Expressions;
using ToRead.Library.Models;

namespace ToRead.Library.Services
{
    /// <summary>
    /// 待读项存储
    /// </summary>
    /// <remarks>该类需要重新实现方法功能</remarks>
    //TODO 重点重构对象
    public class ToReadItemStorage : IToReadItemStorage
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

        public ToReadItemStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        public bool IsInitialized =>
            _preferenceStorage.Get(ToReadItemStorageConstant.VersionKey,
                default(int)) == ToReadItemStorageConstant.Version;

        public async Task InitializeAsync()
        {
            await using var dbFileStream =
                new FileStream(PoetryDbPath, FileMode.OpenOrCreate);
            await using var dbAssetStream =
                typeof(ToReadItemStorage).Assembly.GetManifestResourceStream(DbName) ??
                throw new Exception($"Manifest not found: {DbName}");
            await dbAssetStream.CopyToAsync(dbFileStream);

            _preferenceStorage.Set(ToReadItemStorageConstant.VersionKey,
                ToReadItemStorageConstant.Version);
        }

        public Task<ToReadItem> GetToreadAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<ToReadItem> GetPoetryAsync(int id) =>
            Connection.Table<ToReadItem>().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<ToReadItem>> GetPoetriesAsync(
            Expression<Func<ToReadItem, bool>> where, int skip, int take) =>
            await Connection.Table<ToReadItem>().Where(where).Skip(skip).Take(take)
                .ToListAsync();

        public async Task CloseAsync() => await Connection.CloseAsync();
    }
}