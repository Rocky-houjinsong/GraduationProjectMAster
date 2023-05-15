namespace ToRead.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void RegisterButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}