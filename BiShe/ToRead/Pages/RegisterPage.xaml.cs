namespace ToRead.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private void RegisterButton_OnClicked(object sender, EventArgs e)
    {
        DisplayAlert("无法注册", "未链接数据库服务,", "OK");
    }
}