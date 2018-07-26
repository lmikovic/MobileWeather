using System;
using Xamarin.Forms;

namespace MobileWeather.Core.Services.Interfaces
{
    public interface IViewFactory
    {
        Page Resolve(Type pageType);
        Page Resolve(Type pageType, object parameter);
    }
}
