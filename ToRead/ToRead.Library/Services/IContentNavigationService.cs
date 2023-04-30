namespace ToRead.Library.Services
{
    /// <summary>
    /// 内容导航服务接口;
    /// </summary>
    /// <remarks>该接口调教完成</remarks>
    public interface IContentNavigationService
    {
        /// <summary>
        /// 无参导航
        /// </summary>
        /// <param name="pageKey"></param>
        Task NavigateToAsync(string pageKey);

        /// <summary>
        /// 带参导航
        /// </summary>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        Task NavigateToAsync(string pageKey, object parameter);
    }
}