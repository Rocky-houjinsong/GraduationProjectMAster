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

    #region 登录功能

    #region 属性/事件

    /*属性 用户名 ,密码 封装 私有 字段, 属性改变事件*/
    private string _email;
    private string _password;
    public event PropertyChangedEventHandler PropertyChanged;
    private string _errorMessage; //异常信息;
    public Command LoginCommand { get; }

    /// <summary>
    /// 用户名
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
    /// 密码
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
    /// 异常信息
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
        // 发起登录请求
        HttpClient httpClient = new HttpClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("email", Email),
            new KeyValuePair<string, string>("password", Password)
        });

        //--------- 填充项的处理; -------------------
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
            //存储 JWT令牌
            SecureStorage.SetAsync("jwt_token", token);

            // 在 其他API调用中 使用JWT令牌进行身份验证

            if (response.IsSuccessStatusCode)
            {
                // 登录成功，导航到主页
                StateLabel.Text = "登录成功";
            }
            else
            {
                // 登录失败，显示错误消息
                StateLabel.Text = "登录失败";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    #endregion
}