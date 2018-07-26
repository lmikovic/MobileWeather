using MobileWeather.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileWeather.Core.Services
{
    public class NavigationService : INavigationService
    {
        private static Dictionary<Type, Type> _pagesByType = new Dictionary<Type, Type>();
        private readonly IViewFactory _viewFactory;

        public NavigationService(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Register<TView, TViewModel>()
        {
            _pagesByType.Add(typeof(TView), typeof(TViewModel));
        }

        public Task NavigateAsync<TViewModel>(bool animated = true)
        {
            return NavigateToAsync(typeof(TViewModel), null, animated);
        }

        public Task NavigateAsync<TViewModel>(object parameter, bool animated = true)
        {
            return NavigateToAsync(typeof(TViewModel), parameter, animated);
        }

        private async Task NavigateToAsync(Type viewModelType, object parameter, bool animated)
        {
            Page page = CreatePage(viewModelType, parameter);

            var navigationPage = Application.Current.MainPage as NavigationPage;
            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page, animated);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
        }

        public void SetRootPage(Type viewModelType)
        {
            Page page = CreatePage(viewModelType);
            Application.Current.MainPage = new NavigationPage(page);
        }

        private Page CreatePage(Type viewModelType, object parameter = null)
        {
            Type pageType = _pagesByType.FirstOrDefault(x => x.Value == viewModelType).Key;
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType.Name}");
            }

            Page page = parameter != null ? _viewFactory.Resolve(pageType, parameter) :
                _viewFactory.Resolve(pageType);

            return page;
        }
    }
}
