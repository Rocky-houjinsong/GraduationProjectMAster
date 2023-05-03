using System.Linq.Expressions;
using ToRead.Library.Models;

namespace ToRead.Library.Services
{
    #region 增删改查

    /*
     * 初始化--ok
     * 是否 初始化--ok
     * 增加
     * 删除
     * 修改
     * 作废
     *
     */

    #endregion

    /// <summary>
    /// 待读项存储接口
    /// </summary>
    /// <remarks>该接口 需要调教</remarks>
    //TODO 对待读项 功能模块的设计和实现
    public interface IToReadItemStorage
    {
        /// <summary>
        /// 是否初始化标志位.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 初始化操作.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 获取单个数据
        /// </summary>
        Task<ToReadItem> GetToReadItemAsync(int id);

        /// <summary>
        /// 获取特定数据集合
        /// </summary>
        /// <param name="where">搜索条件</param>
        /// <param name="skip">跳过个数</param>
        /// <param name="take">读取个数</param>
        /// <returns></returns>
        Task<IEnumerable<ToReadItem>> GetToReadsAsync(
            Expression<Func<ToReadItem, bool>> where, int skip, int take);

        /*/// <summary>
        /// 作废标志位
        /// </summary>
        bool IsInValid { get; }

        /// <summary>
        /// 作废
        /// </summary>
        Task InValid(int id);

        bool IsDelete { get; }
        Task DeleteAsync(int id);

        /// <summary>
        /// 修改操作
        /// </summary>
        Task<ToReadItem> UpdateAsync(int id);

        /// <summary>
        /// 新增操作
        /// </summary>
        Task<ToReadItem> NewAsync(int id);*/
    }
}