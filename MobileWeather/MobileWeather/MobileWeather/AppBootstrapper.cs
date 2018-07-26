using Autofac;
using MobileWeather.Core.Services;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using MobileWeather.ViewModels;
using MobileWeather.Views;
using System;
using Xamarin.Forms;

namespace MobileWeather
{
    public class AppBootstrapper
    {
        IContainer container;

        public void Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ViewFactory>().As<IViewFactory>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<RequestService>().As<IRequestService>().SingleInstance();
            builder.RegisterType<RuntimeContext>().As<IRuntimeContext>().SingleInstance();
            builder.RegisterType<LocationService>().As<ILocationService>().SingleInstance();

            builder.RegisterType<ApixuService>().SingleInstance();
            builder.RegisterType<DarkskyService>().SingleInstance();
            builder.RegisterType<WeatherbitService>().SingleInstance();

            builder.Register<IWeatherService>(c =>
            {
                ServicesEnum weatherServiceType = (ServicesEnum)Enum.Parse(typeof(ServicesEnum), AppSettings.SelectedWeatherServices);
                var twoLetterISOLanguageName = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                switch (weatherServiceType)
                {
                    case ServicesEnum.Apixu:
                        return c.Resolve<ApixuService>(new NamedParameter("lang", twoLetterISOLanguageName));
                    case ServicesEnum.Weatherbit:
                        return c.Resolve<WeatherbitService>(new NamedParameter("lang", twoLetterISOLanguageName));
                    case ServicesEnum.Darksky:
                        return c.Resolve<DarkskyService>(new NamedParameter("lang", twoLetterISOLanguageName));
                    default:
                        return c.Resolve<ApixuService>(new NamedParameter("lang", twoLetterISOLanguageName));
                }
            });

            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<WeatherViewModel>();
            builder.RegisterType<Settings>();
            builder.RegisterType<Weather>();

            container = builder.Build();

            //Mapping views and viewmodels in navigation service
            container.Resolve<INavigationService>().Register<Settings, SettingsViewModel>();
            container.Resolve<INavigationService>().Register<Weather, WeatherViewModel>();

            var runtimeContext = container.Resolve<IRuntimeContext>();

            if (IsSettingsEmpty(runtimeContext))
            {
                var page = container.Resolve<Settings>();
                Application.Current.MainPage = new NavigationPage(page);
            }
            else
            {
                var page = container.Resolve<Weather>();
                Application.Current.MainPage = new NavigationPage(page);
            }
        }

        private bool IsSettingsEmpty(IRuntimeContext runtimeContext)
        {
            return (string.IsNullOrEmpty(runtimeContext.SelectedWeatherServices) && string.IsNullOrEmpty(runtimeContext.CityName));
        }
    }
}
