using ToRead.Library.Models;
using ToRead.ViewModels;

namespace ToRead.Pages;

public partial class NewItemPage : ContentPage
{
    public Item Item { get; set; }

    public NewItemPage()
    {
        InitializeComponent();
        BindingContext = new NewItemViewModel();
    }
}