using System.ComponentModel;
using ToRead.Modules.Pages;

namespace ToRead.Pages;

public partial class LoginPage : ContentPage, INotifyPropertyChanged
{
    public LoginPage()
    {
        InitializeComponent();
        LoginCommand = new Command(async () => await LoginAsync());
        BindingContext = this;
    }

    private async void RegisterButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    #region ��¼����

    #region ����/�¼�

    /*���� �û��� ,���� ��װ ˽�� �ֶ�, ���Ըı��¼�*/
    private string _email;
    private string _password;
    public event PropertyChangedEventHandler PropertyChanged;
    private string _errorMessage; //�쳣��Ϣ;
    public Command LoginCommand { get; }

    /// <summary>
    /// �û���
    /// </summary>
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
        }
    }

    /// <summary>
    /// �쳣��Ϣ
    /// </summary>
    public string ErrorMessage
    {
        get { return _errorMessage; }
        set
        {
            _errorMessage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
        }
    }

    #endregion


    public async Task LoginAsync()
    {
        // �����¼����
        HttpClient httpClient = new HttpClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("email", Email),
            new KeyValuePair<string, string>("password", Password)
        });

        //--------- �����Ĵ���; -------------------
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            ErrorMessage = "Please enter your email and password.";
            return;
        }

        try
        {
            // Make API call to authenticate user
            var response = await httpClient.PostAsync("https://localhost:7264/api/Authentication/login", content);

            var token = await response.Content.ReadAsStringAsync();
            //�洢 JWT����
            SecureStorage.SetAsync("jwt_token", token);

            // �� ����API������ ʹ��JWT���ƽ��������֤

            if (response.IsSuccessStatusCode)
            {
                // ��¼�ɹ�����������ҳ
                StateLabel.Text = "��¼�ɹ�";
            }
            else
            {
                // ��¼ʧ�ܣ���ʾ������Ϣ
                StateLabel.Text = "��¼ʧ��";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    #endregion
}