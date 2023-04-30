namespace ToRead;

using ToRead.Services;

class ServiceLocator
{
    private IServiceProvider _serviceProvider;

    /*public IToReadItemStorage toreadItemStorage =>
        _serviceProvider.GetService<IToReadItemStorage>();

    public IToReadFavoriteStorage toreadFavoriteStorage =>
        _serviceProvider.GetService<IToReadFavoriteStorage>();

    public IInitializationNavigationService InitializationNavigationService =>
        _serviceProvider.GetService<IInitializationNavigationService>();*/

    public IRouteService RouteService =>
        _serviceProvider.GetService<IRouteService>();

    //构造函数 依赖注入容器
    public ServiceLocator()
    {
        var serviceCollection = new ServiceCollection();
        /*serviceCollection.AddSingleton<IPreferenceStorage, PreferenceStorage>();
        serviceCollection.AddSingleton<IToReadItemStorage, ToReadItemStorage>();
        serviceCollection.AddSingleton<IToReadFavoriteStorage, ToReadFavoriteStorage>();
        serviceCollection
            .AddSingleton<IInitializationNavigationService,
                InitializationNavigationService>();
        serviceCollection.AddSingleton<IRouteService, RouteService>();*/

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}