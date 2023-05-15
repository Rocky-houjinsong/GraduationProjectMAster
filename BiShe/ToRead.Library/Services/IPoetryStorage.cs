using System.Linq.Expressions;
using ToRead.Models;

namespace ToRead.Services;

public interface IPoetryStorage
{
    bool IsInitialized { get; }

    Task InitializeAsync();

    Task<Poetry> GetPoetryAsync(int id);

    Task<IEnumerable<Poetry>> GetPoetriesAsync(
        Expression<Func<Poetry, bool>> where, int skip, int take);

    Task AddPoetryAsync(TodayPoetry poetry);
}