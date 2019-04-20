using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamEssentials.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public IPageDialogService PageDialogService { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            PageDialogService = dialogService;
        }

        bool IsLightOn { get; set; }

        public GPSLocation GPSLocation { get; set; } = new GPSLocation();

        DelegateCommand getLocationCommand;
        public DelegateCommand GetLocationCommand =>
            getLocationCommand ?? (getLocationCommand = new DelegateCommand(async () => await GetLocationAsync()));

        async Task GetLocationAsync()
        {
            try
            {
                GPSLocation.Longitude = "Loading...";
                GPSLocation.Latitude = "Loading...";
                GPSLocation.Altitude = "Loading...";
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    GPSLocation.Longitude = location.Longitude.ToString("F4");
                    GPSLocation.Latitude = location.Latitude.ToString("F4");
                    GPSLocation.Altitude = location.Altitude.HasValue ? location.Altitude.Value.ToString("F4") : "???";
                }
                else
                {
                    GPSLocation.Longitude = "No location";
                    GPSLocation.Latitude = "No location";
                    GPSLocation.Altitude = "No location";
                }
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Error", $"Unable to get location. {ex.Message}", "Cancel");
            }
        }

        DelegateCommand toggleLightCommand;
        public DelegateCommand ToggleLightCommand =>
            toggleLightCommand ?? (toggleLightCommand = new DelegateCommand(async () => await ToggleLightAsync()));

        async Task ToggleLightAsync()
        {
            try
            {
                if (IsLightOn)
                {
                    await Flashlight.TurnOffAsync();
                    IsLightOn = false;
                }
                else
                {
                    await Flashlight.TurnOnAsync();
                    IsLightOn = true;
                }
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Error", $"Unable to manage lights. {ex.Message}", "Cancel");
            }
        }

        private DelegateCommand vibrateCommand;
        public DelegateCommand VibrateCommand =>
            vibrateCommand ?? (vibrateCommand = new DelegateCommand(Vibrate));

        void Vibrate()
        {
            try
            {
                var duration = TimeSpan.FromSeconds(3);
                Vibration.Vibrate(duration);
            }
            catch (Exception ex)
            {
                PageDialogService.DisplayAlertAsync("Error", $"Unable to get vibration. {ex.Message}", "Cancel");
            }
        }
    }
}
