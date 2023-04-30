using System.Collections.ObjectModel;
using ToRead.Library.Models;

namespace ToRead.DetailPage;

public partial class InboxDetailPage : ContentPage
{
    private string contentUri = "https://thesimpsonsquoteapi.glitch.me";
    public ObservableCollection<Simpson> Simpsons = new ObservableCollection<Simpson>();

    public InboxDetailPage()
    {
        InitializeComponent();
        MessageCollection.ItemsSource = Simpsons;
    }

    protected override async void OnAppearing()
    {
        LoadingIndicator.IsVisible = true;
        base.OnAppearing();
        var httpClient = new HttpClient();
        // var jsonResponse = await httpClient.GetFromJsonAsync(contentUri);
        LoadingIndicator.IsVisible = false;
    }
}