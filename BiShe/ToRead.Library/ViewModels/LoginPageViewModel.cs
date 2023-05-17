using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using ToRead.Services;

namespace ToRead.ViewModels
{
    public class LoginPageViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region 导航功能

        IContentNavigationService _contentNavigationService;
        private IRootNavigationService _rootNavigationService;

        public LoginPageViewModel(IContentNavigationService contentNavigationService,
            IRootNavigationService rootNavigationService)
        {
            _contentNavigationService = contentNavigationService;
            _rootNavigationService = rootNavigationService;
        }

        #endregion

        /*#region 登录功能

        #region 属性/事件

        /*属性 用户名 ,密码 封装 私有 字段, 属性改变事件#1#
        private string _email;
        private string _password;
        public event PropertyChangedEventHandler PropertyChanged;
        private string _errorMessage; //异常信息;

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

                // If successful, navigate to main page
                //   await Navigation.PushAsync(new NavigationByButtonPage());  弹出警告
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            if (response.IsSuccessStatusCode)
            {
                // 登录成功，导航到主页
            }
            else
            {
                // 登录失败，显示错误消息
            }
        }

        #endregion*/
    }
}