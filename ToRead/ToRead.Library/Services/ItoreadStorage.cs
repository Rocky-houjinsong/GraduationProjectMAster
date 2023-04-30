using System.Linq.Expressions;
using ToRead.Library.Models;

namespace ToRead.Library.Services
{
    public interface ItoreadStorage
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
        Task<ToReadItem> GettoreeadAsync(int id);

        Task<IEnumerable<ToReadItem>> GetPoetriesAsync(
            Expression<Func<ToReadItem, bool>> where, int skip, int take);
    }
}