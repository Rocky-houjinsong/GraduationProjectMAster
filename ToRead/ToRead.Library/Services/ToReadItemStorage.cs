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

        public Task<ToReadItem> GetToreadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToReadItem>> GetToreadsAsync(Expression<Func<ToReadItem, bool>> where, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public bool IsInValid { get; }
        public Task InValid(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsDelete { get; }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ToReadItem> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ToReadItem> NewAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Poetry> GetPoetryAsync(int id) =>
            Connection.Table<Poetry>().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Poetry>> GetPoetriesAsync(
            Expression<Func<Poetry, bool>> where, int skip, int take) =>
            await Connection.Table<Poetry>().Where(where).Skip(skip).Take(take)
                .ToListAsync();

        public async Task CloseAsync() => await Connection.CloseAsync();
    }