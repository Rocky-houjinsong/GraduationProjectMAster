namespace ToRead.Library.Services
{
    /// <summary>
    /// 初始化导航服务接口
    /// </summary>
    /// <remarks>该接口调教完成</remarks>
    public interface IInitializationNavigationService
    {
        void NavigateToInitializationPage();

        void NavigateToAppShell();
    }
}