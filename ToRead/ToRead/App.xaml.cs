namespace ToRead;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        //TODO  内置Shell导航  和  自建导航栈
    }
}