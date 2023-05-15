namespace ToRead.Services;

public interface IRootNavigationService
{
    Task NavigateToAsync(string pageKey);

    Task NavigateToAsync(string pageKey, object parameter);
}

//TODO 根导航常量
public static class RootNavigationConstant
{
    public const string TodayPage = nameof(TodayPage);

    public const string QueryPage = nameof(QueryPage);

    public const string FavoritePage = nameof(FavoritePage);

    public const string AboutPage = nameof(AboutPage);

    public const string LoginPage = nameof(LoginPage);
    public const string RegisterPage = nameof(RegisterPage);
}