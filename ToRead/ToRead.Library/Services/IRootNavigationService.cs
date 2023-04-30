namespace ToRead.Library.Services
{
    /// <summary>
    /// 根导航服务接口
    /// </summary>
    /// <remarks>该接口调教完成</remarks>
    public interface IRootNavigationService
    {
        Task NavigateToAsync(string pageKey);

        Task NavigateToAsync(string pageKey, object parameter);
    }
}