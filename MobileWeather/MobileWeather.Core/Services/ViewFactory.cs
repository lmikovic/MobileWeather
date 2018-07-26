using Autofac;
using MobileWeather.Core.Services.Interfaces;
using System;
using Xamarin.Forms;

namespace MobileWeather.Core.Services
{
    public class ViewFactory : IViewFactory
    {
        private readonly IComponentContext _componentContext;

        public ViewFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public Page Resolve(Type pageType)
        {
            return _componentContext.Resolve(pageType) as Page;
        }

        public Page Resolve(Type pageType, object parameter)
        {
            return _componentContext.Resolve(pageType, new NamedParameter("parameter", parameter)) as Page;
        }
    }
}
