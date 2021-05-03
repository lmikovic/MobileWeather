using Autofac;
using MobileWeather.Core.Services;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using MobileWeather.ViewModels;
using MobileWeather.Views;
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

            builder.RegisterType<DarkskyService>().Named<IWeatherService>("Darksky");
            builder.RegisterType<WeatherbitService>().Named<IWeatherService>("Weatherbit");
            builder.RegisterType<AmbeeService>().Named<IWeatherService>("Ambee");
            builder.RegisterType<AccuweatherService>().Named<IWeatherService>("Accuweather");
            builder.RegisterType<OpenWeatherMapService>().Named<IWeatherService>("OpenWeatherMap");

            builder.Register(c =>
            {
                var twoLetterISOLanguageName = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                return c.ResolveNamed<IWeatherService>(AppSettings.SelectedWeatherServices, new NamedParameter("lang", twoLetterISOLanguageName), new NamedParameter("isImperial", AppSettings.IsImperial));
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
