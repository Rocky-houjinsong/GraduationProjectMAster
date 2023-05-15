namespace ToRead.Modules.GestureView;

public partial class SwipeGestureView : ContentPage
{
    public SwipeGestureView()
    {
        InitializeComponent();
    }

    private async void OnSwiped(object sender, SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Left:
                // Handle the swipe
                await DisplayAlert("Left", sender.ToString(), "OK");
                break;
            case SwipeDirection.Right:
                // Handle the swipe
                await DisplayAlert("Right!", sender.ToString(), "OK");
                break;
            case SwipeDirection.Up:
                // Handle the swipe
                await DisplayAlert("Up!", sender.ToString(), "OK");
                break;
            case SwipeDirection.Down:
                // Handle the swipe
                await DisplayAlert("Down!", sender.ToString(), "OK");
                break;
        }
    }
}