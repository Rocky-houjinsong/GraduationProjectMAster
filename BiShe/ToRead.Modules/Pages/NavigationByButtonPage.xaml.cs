namespace ToRead.Modules.Pages;

public partial class NavigationByButtonPage : ContentPage
{
    public NavigationByButtonPage()
    {
        InitializeComponent();
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        valueLabel.Text = args.NewValue.ToString("F3"); // �¼�����args��ȡ ֵ
        // valueLabel.Text = ((Slider)sender).Value.ToString("F3");  // ����sender����¼��Ķ��� 
    }

    async void OnButtonClicked(object sender, EventArgs args)
    {
        Button button = (Button)sender;
        await DisplayAlert("Clicked!", "The button labeled '" + button.Text + "' has been clicked", "OK");
    }
}