using ToRead.Models;
using ToRead.Services;

namespace ToRead.Pages;

public partial class TodayDetailPage : ContentPage
{
    public TodayDetailPage()
    {
        InitializeComponent();
    }


    private async void ShareClicked(object sender, EventArgs e)
    {
        try
        {
            // 生成分享的文本字符串
            string shareText = "https://www.jinrishici.com";

            // 启动分享功能，将文本字符串分享给其他应用程序
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = shareText,
                Title = "Share Text"
            });
        }
        catch (Exception ex)
        {
            // 发生错误时，显示错误信息
            await DisplayAlert("Error", $"An error occurred while sharing: {ex.Message}", "OK");
        }
    }
}