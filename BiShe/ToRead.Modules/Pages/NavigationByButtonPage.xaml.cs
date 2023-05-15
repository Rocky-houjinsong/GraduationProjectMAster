namespace ToRead.Modules.Pages;

public partial class NavigationByButtonPage : ContentPage
{
    public NavigationByButtonPage()
    {
        InitializeComponent();
    }

    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        valueLabel.Text = args.NewValue.ToString("F3"); // 事件参数args获取 值
        // valueLabel.Text = ((Slider)sender).Value.ToString("F3");  // 参数sender获得事件的对象 
    }

    async void OnButtonClicked(object sender, EventArgs args)
    {
        Button button = (Button)sender;
        await DisplayAlert("Clicked!", "The button labeled '" + button.Text + "' has been clicked", "OK");
    }
}