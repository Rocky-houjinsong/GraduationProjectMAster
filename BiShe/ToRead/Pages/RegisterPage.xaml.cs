namespace ToRead.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private void RegisterButton_OnClicked(object sender, EventArgs e)
    {
        DisplayAlert("�޷�ע��", "δ�������ݿ����,", "OK");
    }
}