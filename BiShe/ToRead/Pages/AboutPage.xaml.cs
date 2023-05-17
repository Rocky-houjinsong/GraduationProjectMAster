namespace ToRead.Pages;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void ModelsPageOnClieked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModelsPage());
    }

    private async void ControlsPageOnClieked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ControlsPage());
    }
}