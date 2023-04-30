namespace ToRead;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        //TODO 后台如何 进行建立导航栈的?
    }

    private void AddFlyoutItem(string title, string route, Type type) =>
        Items.Add(new FlyoutItem
        {
            Title = title,
            Route = route,
            Items =
            {
                new ShellContent { ContentTemplate = new DataTemplate(type) }
            }
        });
}