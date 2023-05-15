namespace DeviceAPI.Services;

public class ShareTodayPoetry
{
    private async void ShareObjectValue(object value)
    {
        if (value != null)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = value.ToString(),
                Title = "Share Object Value"
            });
        }
    }
}