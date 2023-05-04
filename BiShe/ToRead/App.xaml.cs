namespace ToRead;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //放弃 内置,高封装的Shell导航 
        //MainPage = new  AppShell();
        //改用 MAUI原有的导航机制,自建导航栈
        MainPage = new NavigationPage(new ContentPage());
        //获取自建 依赖容器名
        var serviceLocatorName = nameof(ServiceLocator);
        //反射,根据 依赖容器名,依赖容器对象实例化
        var serviceLocator =
            (ServiceLocator)Application.Current.Resources.MergedDictionaries
                .First(p => p.ContainsKey(serviceLocatorName))[
                    serviceLocatorName];
        //以下是 根据 依赖容器对象实例化 
        var poetryStorage = serviceLocator.PoetryStorage;
        var favoriteStorage = serviceLocator.FavoriteStorage;
        var initializationNavigationService = serviceLocator.InitializationNavigationService;

        if (!poetryStorage.IsInitialized || !favoriteStorage.IsInitialized)
        {
            initializationNavigationService.NavigateToInitializationPage();
        }
        else
        {
            initializationNavigationService.NavigateToAppShell();
        }
    }
}