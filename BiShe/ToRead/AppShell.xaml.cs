using ToRead.Pages;
using ToRead.Services;

namespace ToRead;

public partial class AppShell : Shell
{
    //TODO 页面管理
    public AppShell()
    {
        InitializeComponent();

        var serviceLocatorName = nameof(ServiceLocator);
        var serviceLocator =
            (ServiceLocator)Application.Current.Resources.MergedDictionaries.First(p =>
                p.ContainsKey(serviceLocatorName))[serviceLocatorName];
        var routeService = serviceLocator.RouteService;

        AddFlyoutItem("今日推荐",
            routeService.GetRoute(RootNavigationConstant.TodayPage),
            typeof(TodayPage), "recommend.png");
        Routing.RegisterRoute(
            routeService.GetRoute(ContentNavigationConstant.TodayDetailPage),
            typeof(TodayDetailPage));

        AddFlyoutItem("诗词搜索",
            routeService.GetRoute(RootNavigationConstant.QueryPage),
            typeof(QueryPage), "searchpage.png");
        Routing.RegisterRoute(
            routeService.GetRoute(ContentNavigationConstant.ResultPage),
            typeof(ResultPage));
        Routing.RegisterRoute(
            routeService.GetRoute(ContentNavigationConstant.DetailPage),
            typeof(DetailPage));

        AddFlyoutItem("诗词收藏",
            routeService.GetRoute(RootNavigationConstant.FavoritePage),
            typeof(FavoritePage), "collectionpage.png");
        Routing.RegisterRoute(
            routeService.GetRoute(ContentNavigationConstant.FavoriteDetailPage),
            typeof(DetailPage));

        AddFlyoutItem("关于",
            routeService.GetRoute(RootNavigationConstant.AboutPage),
            typeof(AboutPage), "about.png");
        /*AddFlyoutItem("登录",
            routeService.GetRoute(RootNavigationConstant.LoginPage),
            typeof(LoginPage), "login.png");
        AddFlyoutItem("注册",
            routeService.GetRoute(RootNavigationConstant.RegisterPage),
            typeof(RegisterPage), "register.png");*/
    }

    /// <summary>
    /// 创建FlyoutItem,含单个Items
    /// </summary>
    /// <param name="title"></param>
    /// <param name="route"></param>
    /// <param name="type"></param>
    private void AddFlyoutItem(string title, string route, Type type, string icon) =>
        Items.Add(new FlyoutItem
        {
            Title = title,
            Route = route,
            Items =
            {
                new ShellContent { ContentTemplate = new DataTemplate(type) }
            },
            Icon = icon
        });
}