using ToRead.Pages;

namespace ToRead.Services;

public class
    InitializationNavigationService : IInitializationNavigationService
{
    private Lazy<InitializationPage> _lazyInitializationPage =
        new(() => new InitializationPage());

    //当 懒式加载失败,即 软件部署初始化失败,自动启用 默认的Shell导航机制
    //不会因为 加载失败,而程序崩溃退出.
    private Lazy<AppShell> _lazyAppShell = new(() => new AppShell());

    public void NavigateToInitializationPage() =>
        Application.Current!.MainPage = _lazyInitializationPage.Value;

    public void NavigateToAppShell() =>
        Application.Current!.MainPage = _lazyAppShell.Value;
}