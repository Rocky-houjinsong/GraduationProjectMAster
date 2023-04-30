namespace ToRead.Services;

using ToRead.Constants;

/// <summary>
/// 路由服务
/// </summary>
//TODO 理清如何 路由的 , 中等难度
public class RouteService : IRouteService
{
    public string GetRoute(string pageKey) => _routeDictionary[pageKey];

    private readonly Dictionary<string, string> _routeDictionary = new()
    {
        [RootNavigationConstant.AboutPage] = RootNavigationConstant.AboutPage,
        [ContentNavigationConstant.TodayDetailPage] =
            $"{RootNavigationConstant.AboutPage}/{ContentNavigationConstant.TodayDetailPage}"
    };
}