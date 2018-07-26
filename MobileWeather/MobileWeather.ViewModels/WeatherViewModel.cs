using MobileWeather.Core.Models;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileWeather.ViewModels
{
    public class WeatherViewModel : ViewModelBase
    {
        private readonly IWeatherService _weatherService;

        public Command SettingsCommand { get; }
        public Command RefreshCommand { get; }
        public Command ExitCommand { get; }

        public WeatherViewModel(INavigationService navigationService, IWeatherService weatherService)
            : this(navigationService, weatherService, new RuntimeContext())
        { }

        public WeatherViewModel(INavigationService navigationService, IWeatherService weatherService, IRuntimeContext runtimeContext)
            : base(navigationService, runtimeContext)
        {
            _weatherService = weatherService;

            SettingsCommand = new Command(async () => await Settings());
            RefreshCommand = new Command(async () => await Refresh());
            ExitCommand = new Command(() => Exit());
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        private string _weatherIcon;
        public string WeatherIcon
        {
            get { return _weatherIcon; }
            set
            {
                _weatherIcon = value;
                OnPropertyChanged();
            }
        }

        private string _temperature;
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        private string _tempUnit;
        public string TempUnit
        {
            get { return _tempUnit; }
            set
            {
                _tempUnit = value;
                OnPropertyChanged();
            }
        }

        private DateTime _lastUpdate;
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set
            {
                _lastUpdate = value;
                OnPropertyChanged();
            }
        }

        private string _pressure;
        public string Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                OnPropertyChanged();
            }
        }

        private string _windSpeed;
        public string WindSpeed
        {
            get { return _windSpeed; }
            set
            {
                _windSpeed = value;
                OnPropertyChanged();
            }
        }

        private string _humidity;
        public string Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Weather> _forecastList;
        public ObservableCollection<Weather> ForecastList
        {
            get { return _forecastList; }
            set
            {
                _forecastList = value;
                OnPropertyChanged();
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                await LoadWeather();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Refresh()
        {
            try
            {
                IsRefreshing = true;

                await LoadWeather(isRefreshing:true);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task LoadWeather(bool isRefreshing = false)
        {
            WeatherData currentWeather = isRefreshing || string.IsNullOrEmpty(_runtimeContext.CurrentWeather) ?
                await _weatherService.GetWeather(_runtimeContext.CityName, _runtimeContext.IsImperial) :
                JsonConvert.DeserializeObject<WeatherData>(_runtimeContext.CurrentWeather);

            City = currentWeather.City.Name;
            WeatherIcon = currentWeather.Weather.Icon;
            Temperature = currentWeather.Weather.TemperatureCurrent.ToString();
            TempUnit = _runtimeContext.IsImperial ? "°F" : "°C";
            Pressure = currentWeather.Weather.Pressure.ToString();
            WindSpeed = currentWeather.Weather.WindSpeed.ToString();
            Humidity = currentWeather.Weather.Humidity.ToString();
            Description = currentWeather.Weather.WeatherDescription;

            var weatherForecast = isRefreshing || string.IsNullOrEmpty(_runtimeContext.WeatherForecast) ?
                await _weatherService.GetForecast(_runtimeContext.CityName, 5, _runtimeContext.IsImperial) :
                JsonConvert.DeserializeObject<WeatherForecast>(_runtimeContext.WeatherForecast);

            ForecastList = new ObservableCollection<Weather>(weatherForecast.WeatherList);

            if (isRefreshing || string.IsNullOrEmpty(_runtimeContext.CurrentWeather) || string.IsNullOrEmpty(_runtimeContext.WeatherForecast))
            {
                _runtimeContext.UpdateTime = DateTime.Now;
            }

            LastUpdate = _runtimeContext.UpdateTime;

            _runtimeContext.CurrentWeather = JsonConvert.SerializeObject(currentWeather);
            _runtimeContext.WeatherForecast = JsonConvert.SerializeObject(weatherForecast);
        }

        private async Task Settings()
        {
            await _navigationService.NavigateAsync<SettingsViewModel>();
        }

        private void Exit()
        {
            Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}
