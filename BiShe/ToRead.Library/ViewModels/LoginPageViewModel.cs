using CommunityToolkit.Mvvm.ComponentModel;
using ToRead.Services;

namespace ToRead.ViewModels
{
    public class LoginPageViewModel : ObservableObject
    {
        IContentNavigationService _contentNavigationService;
        private IRootNavigationService _rootNavigationService;

        public LoginPageViewModel(IContentNavigationService contentNavigationService, IRootNavigationService rootNavigationService)
        {
            _contentNavigationService = contentNavigationService;
            _rootNavigationService = rootNavigationService;
        }
    }
}
