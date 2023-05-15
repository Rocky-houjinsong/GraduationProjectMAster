using CommunityToolkit.Mvvm.ComponentModel;
using ToRead.Services;

namespace ToRead.ViewModels
{
    public class RegisterPageViewModel : ObservableObject
    {
        IContentNavigationService _contentNavigationService;
        private IRootNavigationService _rootNavigationService;

        public RegisterPageViewModel(IContentNavigationService contentNavigationService, IRootNavigationService rootNavigationService)
        {
            _contentNavigationService = contentNavigationService;
            _rootNavigationService = rootNavigationService;
            
        }
    }
}
