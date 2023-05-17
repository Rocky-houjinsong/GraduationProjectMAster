using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using ToRead.Services;

namespace ToRead.ViewModels
{
    public class RegisterPageViewModel : ObservableObject, INotifyPropertyChanged
    {
        #region 导航功能

        IContentNavigationService _contentNavigationService;
        private IRootNavigationService _rootNavigationService;

        public RegisterPageViewModel(IContentNavigationService contentNavigationService,
            IRootNavigationService rootNavigationService)
        {
            _contentNavigationService = contentNavigationService;
            _rootNavigationService = rootNavigationService;
        }

        #endregion

        #region 注册功能

        #region 属性/事件

        private string _email;
        private string _password;
        private string _confirmPassword;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConfirmPassword)));
            }
        }

        #endregion

        public async Task RegisterAsync()
        {
            HttpClient httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", Email),
                new KeyValuePair<string, string>("password", Password),
                new KeyValuePair<string, string>("confirmPassword", ConfirmPassword)
            });
            var response = await httpClient.PostAsync("https://localhost:7264/api/Authentication/register", content);
            if (response.IsSuccessStatusCode)
            {
                // 注册成功，导航到登录页
            }
            else
            {
                // 注册失败，显示错误消息
            }
        }

        #endregion
    }
}