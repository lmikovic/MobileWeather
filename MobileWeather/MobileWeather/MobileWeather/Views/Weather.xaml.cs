using MobileWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Weather : ContentPage
	{
        WeatherViewModel _weatherViewModel;

        public Weather(WeatherViewModel weatherViewModel, object parameter = null)
		{
            InitializeComponent();

            _weatherViewModel = weatherViewModel;

            if (Device.RuntimePlatform == Device.UWP)
            {
                ToolbarItems.Remove(Exit);
            }

            BindingContext = _weatherViewModel;
        }

        protected override async void OnAppearing()
        {
            await _weatherViewModel.InitializeAsync();
        }
    }
}