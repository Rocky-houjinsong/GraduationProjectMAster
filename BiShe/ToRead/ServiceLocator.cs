using Microsoft.Extensions.DependencyInjection;
using ToRead.Services;
using ToRead.ViewModels;

namespace ToRead;

//TODO 服务容器
public class ServiceLocator
{
    private IServiceProvider _serviceProvider;

    public TodayPageViewModel TodayPageViewModel =>
        _serviceProvider.GetService<TodayPageViewModel>();

    public QueryPageViewModelProxy QueryPageViewModelProxy =>
        _serviceProvider.GetService<QueryPageViewModelProxy>();

    public ResultPageViewModel ResultPageViewModel =>
        _serviceProvider.GetService<ResultPageViewModel>();

    public InitializationPageViewModel InitializationPageViewModel =>
        _serviceProvider.GetService<InitializationPageViewModel>();

    public DetailPageViewModelProxy DetailPageViewModelProxy =>
        _serviceProvider.GetService<DetailPageViewModelProxy>();

    public FavoritePageViewModel FavoritePageViewModel =>
        _serviceProvider.GetService<FavoritePageViewModel>();

    public AboutPageViewModel AboutPageViewModel =>
        _serviceProvider.GetService<AboutPageViewModel>();

    public LoginPageViewModel LoginPageViewModel =>
        _serviceProvider.GetService<LoginPageViewModel>();

    public RegisterPageViewModel RegisterPageViewModel =>
        _serviceProvider.GetService<RegisterPageViewModel>();

    public IRouteService RouteService =>
        _serviceProvider.GetService<IRouteService>();

    public IPoetryStorage PoetryStorage =>
        _serviceProvider.GetService<IPoetryStorage>();

    public IFavoriteStorage FavoriteStorage =>
        _serviceProvider.GetService<IFavoriteStorage>();

    public IInitializationNavigationService InitializationNavigationService =>
        _serviceProvider.GetService<IInitializationNavigationService>();


    public ServiceLocator()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IPreferenceStorage, PreferenceStorage>();
        serviceCollection.AddSingleton<IAlertService, AlertService>();

        serviceCollection.AddSingleton<IPoetryStorage, PoetryStorage>();

        serviceCollection
            .AddSingleton<ITodayPoetryService, JinrishiciService>();

        serviceCollection.AddSingleton<ITodayImageStorage, TodayImageStorage>();
        serviceCollection.AddSingleton<ITodayImageService, BingImageService>();

        serviceCollection.AddSingleton<IBrowserService, BrowserService>();

        serviceCollection.AddSingleton<IRouteService, RouteService>();
        serviceCollection
            .AddSingleton<IRootNavigationService, RootNavigationService>();
        serviceCollection
            .AddSingleton<IContentNavigationService,
                ContentNavigationService>();

        serviceCollection.AddSingleton<TodayPageViewModel>();
        serviceCollection.AddSingleton<QueryPageViewModelProxy>();
        serviceCollection.AddSingleton<ResultPageViewModel>();
        serviceCollection.AddSingleton<InitializationPageViewModel>();
        serviceCollection.AddSingleton<DetailPageViewModelProxy>();
        serviceCollection.AddSingleton<FavoritePageViewModel>();
        serviceCollection.AddSingleton<AboutPageViewModel>();
        serviceCollection.AddSingleton<LoginPageViewModel>();
        serviceCollection.AddSingleton<RegisterPageViewModel>();
        serviceCollection.AddSingleton<IFavoriteStorage, FavoriteStorage>();
        serviceCollection
            .AddSingleton<IInitializationNavigationService,
                InitializationNavigationService>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}