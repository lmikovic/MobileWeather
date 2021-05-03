using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MobileWeather.Core.Settings
{
    public static class AppSettings
    {
        // Endpoints
        private const string DefaultApixuBaseEndpoint = "http://api.apixu.com";
        private const string DefaultDarkskyBaseEndpoint = "https://api.darksky.net";
        private const string DefaultWeatherbitBaseEndpoint = "http://api.weatherbit.io";
        private const string DefaultAmbeeBaseEndpoint = "https://api.ambeedata.com";
        private const string DefaultAccuweatherBaseEndpoint = "http://dataservice.accuweather.com";
        private const string DefaultOpenWeatherMapBaseEndpoint = "https://api.openweathermap.org";
        private const string DefaultBingLocationBaseEndpoint = "http://dev.virtualearth.net";

        // Keys
        private const string DefaultApixuKey = "9352d4c3623340caa9d135516170309";
        private const string DefaultDarkskyKey = "1becc6edf70846062ff32a277ad56104";
        private const string DefaultWeatherbitKey = "16556b95929f49939fc0c034c6f10070";
        private const string DefaultAmbeeKey = "b3gpXG0tWi64r4DxBiAl77fE8rI5dh1maCwmGqFC";
        private const string DefaultAccuweatherKey = "fneGhKXf54AbGkFq9BvCGe3f3glYfo6V";
        private const string DefaultOpenWeatherMapKey = "c2b5c9b98461214bc2cc6ab607d5455a";
        private const string DefaultBingMapKey = "Aqa9k7WZbF9AvxITJ-4fG_F2po48W4R3vFcxzTFD8iLGQrbFpGKConi1ZqjJ5i_f";

        private static ISettings Settings => CrossSettings.Current;

        // API Endpoints
        public static string ApixuBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(ApixuBaseEndpoint), DefaultApixuBaseEndpoint);
        }

        public static string DarkskyBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(DarkskyBaseEndpoint), DefaultDarkskyBaseEndpoint);
        }

        public static string WeatherbitBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(WeatherbitBaseEndpoint), DefaultWeatherbitBaseEndpoint);
        }

        public static string LocationBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(LocationBaseEndpoint), DefaultBingLocationBaseEndpoint);
        }

        public static string AmbeeBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(AmbeeBaseEndpoint), DefaultAmbeeBaseEndpoint);
        }

        public static string AccuweatherBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(AccuweatherBaseEndpoint), DefaultAccuweatherBaseEndpoint);
        }

        public static string OpenWeatherMapBaseEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(OpenWeatherMapBaseEndpoint), DefaultOpenWeatherMapBaseEndpoint);
        }

        // API Keys
        public static string ApixuKey
        {
            get => Settings.GetValueOrDefault(nameof(ApixuKey), DefaultApixuKey);
        }

        public static string DarkskyKey
        {
            get => Settings.GetValueOrDefault(nameof(DarkskyKey), DefaultDarkskyKey);
        }

        public static string WeatherbitKey
        {
            get => Settings.GetValueOrDefault(nameof(WeatherbitKey), DefaultWeatherbitKey);
        }

        public static string BingMapKey
        {
            get => Settings.GetValueOrDefault(nameof(BingMapKey), DefaultBingMapKey);
        }

        public static string AmbeeKey
        {
            get => Settings.GetValueOrDefault(nameof(AmbeeKey), DefaultAmbeeKey);
        }

        public static string AccuweatherKey
        {
            get => Settings.GetValueOrDefault(nameof(AccuweatherKey), DefaultAccuweatherKey);
        }

        public static string OpenWeatherMapKey
        {
            get => Settings.GetValueOrDefault(nameof(OpenWeatherMapKey), DefaultOpenWeatherMapKey);
        }

        // Variables
        public static string CityName
        {
            get => Settings.GetValueOrDefault(nameof(CityName), default(string));

            set => Settings.AddOrUpdateValue(nameof(CityName), value);
        }

        public static string SelectedWeatherServices
        {
            get => Settings.GetValueOrDefault(nameof(SelectedWeatherServices), default(string));

            set => Settings.AddOrUpdateValue(nameof(SelectedWeatherServices), value);
        }

        public static bool IsImperial
        {
            get => Settings.GetValueOrDefault(nameof(IsImperial), default(bool));

            set => Settings.AddOrUpdateValue(nameof(IsImperial), value);
        }


        // Weather objects
        public static string CurrentWeather
        {
            get => Settings.GetValueOrDefault(nameof(CurrentWeather), default(string));

            set => Settings.AddOrUpdateValue(nameof(CurrentWeather), value);
        }

        public static string WeatherForecast
        {
            get => Settings.GetValueOrDefault(nameof(WeatherForecast), default(string));

            set => Settings.AddOrUpdateValue(nameof(WeatherForecast), value);
        }

        public static string UpdateTime
        {
            get => Settings.GetValueOrDefault(nameof(UpdateTime), default(string));

            set => Settings.AddOrUpdateValue(nameof(UpdateTime), value);
        }

        public static void RemoveCachedWeather()
        {
            Settings.Remove(nameof(CurrentWeather));
            Settings.Remove(nameof(WeatherForecast));
            Settings.Remove(nameof(UpdateTime));
        }
    }
}
