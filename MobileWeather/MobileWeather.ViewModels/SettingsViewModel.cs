using MobileWeather.Core.Services;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileWeather.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IRuntimeContext _runtimeContext;

        public Command SaveCommand { get; }

        public SettingsViewModel(INavigationService navigationService, IRuntimeContext runtimeContext)
            : base(navigationService, runtimeContext)
        {
            _runtimeContext = runtimeContext;

            SaveCommand = new Command(async () => await Save(),
                () => !IsBusy && !string.IsNullOrWhiteSpace(CityName));

            LoadValues();
        }

        public string[] WeahterServiceList => _runtimeContext.GetServices().OrderBy(x => x).ToArray();

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _selectedWeatherServices;
        public string SelectedWeatherServices
        {
            get { return _selectedWeatherServices; }
            set
            {
                _selectedWeatherServices = value;
                OnPropertyChanged();
            }
        }

        private bool _isImperial;
        public bool IsImperial
        {
            get { return _isImperial; }
            set
            {
                _isImperial = value;
                OnPropertyChanged();
            }
        }

        private void LoadValues()
        {
            CityName = _runtimeContext.CityName;
            SelectedWeatherServices = _runtimeContext.SelectedWeatherServices;
            IsImperial = _runtimeContext.IsImperial;
        }

        private void SaveValues()
        {
            _runtimeContext.CityName = CityName;
            _runtimeContext.SelectedWeatherServices = SelectedWeatherServices;
            _runtimeContext.IsImperial = IsImperial;

            _runtimeContext.RemoveCachedWeather();
        }

        private async Task Save()
        {
            try
            {
                IsBusy = true;

                SaveValues();
                _navigationService.SetRootPage(typeof(WeatherViewModel));
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
    }
}
