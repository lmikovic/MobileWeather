using MobileWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings (SettingsViewModel settingsViewModel, object parameter = null)
		{
			InitializeComponent ();

            BindingContext = settingsViewModel;
        }
	}
}