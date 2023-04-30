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
        public bool IsInitialized { get; }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ToReadItem> GetToreadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToReadItem>> GetToreadsAsync(Expression<Func<ToReadItem, bool>> where, int skip,
            int take)
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

        public Task<ToReadItem> GettoreeadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToReadItem>> GetPoetriesAsync(Expression<Func<ToReadItem, bool>> where, int skip,
            int take)
        {
            throw new NotImplementedException();
        }
    }
}