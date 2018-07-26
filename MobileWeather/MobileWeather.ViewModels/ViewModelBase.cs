using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using Xamarin.Forms;

namespace MobileWeather.ViewModels
{
    public abstract class ViewModelBase : BindableObject
    {
        protected readonly INavigationService _navigationService;
        protected readonly IRuntimeContext _runtimeContext;

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ViewModelBase(INavigationService navigationService, IRuntimeContext runtimeContext)
        {
            _navigationService = navigationService;
            _runtimeContext = runtimeContext;
        }
    }
}
