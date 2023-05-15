using System.Reflection;
using ToRead.Modules.Pages;

namespace ToRead.Pages;

public partial class ModelsPage : ContentPage
{
    public ModelsPage()
    {
        InitializeComponent();
    }

    private async void NavigationByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationByButtonPage());
    }

    private async void MarkupExtensionsByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MarkupExtensionsPage());
    }

    private async void DataBindingModeByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DataBindingModePage());
    }

    private async void View2ViewDataBindingByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new View2ViewDataBindingPage());
    }

    private async void DataBindingCollectionsByButtonClicked(object sender, EventArgs e)
    {
        //TODO �󶨵����ݼ��Ϲ���,ÿ�ζ���new �Ļ�, �ڴ�����ͱ�ը ҳ��� ����ʹ�ô�����ʾ��,�����˳�Ҫ����
        await Navigation.PushAsync(new ListViewDataBindingPage());
    }

    private async void SimpleMVVMByButtonByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SimpleMVVMPage());
    }

    private async void SimpleMVVMByClockByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SimpleMVVMByClock());
    }

    private async void InteractiveMVVMByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InteractiveMVVMPage());
    }

    private async void KeypadMVVMByButton_OnClickedeypadMVVMByButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KeypadMVVMPage());
    }
}