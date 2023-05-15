using ToRead.Models;
using ToRead.Services;

namespace ToRead.Pages;

public partial class TodayDetailPage : ContentPage
{
    public TodayDetailPage()
    {
        InitializeComponent();
    }

    //TODO 点击 下载操作 在今日 详情 的ViewModel页 ,构造方法 添加PoetryStorage , 调用该字段的 方法 ; 
    /*private async void DownloadByButton_Clicked(object sender, EventArgs e)
    {
        TodayPoetry poetry = new TodayPoetry
        {
            Name = Label_Name.Text,
            Dynasty = Label_Dynasty.Text,
            Author = Label_Author.Text,
            Content = Label_Content.Text
        };
        //    await PoetryStorage.AddPoetryAsync(poetry);
    }*/
}