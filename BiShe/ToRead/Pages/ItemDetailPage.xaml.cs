using ToRead.ViewModels;

namespace ToRead.Pages;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage()
    {
        InitializeComponent();
        BindingContext = new ItemDetailViewModel();
    }
}