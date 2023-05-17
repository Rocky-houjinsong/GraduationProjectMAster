namespace ToRead.Pages;

public partial class ControlsPage : ContentPage
{
    public ControlsPage()
    {
        InitializeComponent();
    }

    private async void ClickHyperlinkButton_Tapped(object sender, EventArgs e)
    {
        await Browser.OpenAsync("https://docs.microsoft.com/zh-cn/");
    }

    private void MyPopupButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Greetings!", "You have clicked me!", "OK");
    }
}