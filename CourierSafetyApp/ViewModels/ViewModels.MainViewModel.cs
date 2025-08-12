using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourierSafetyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSafetyApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly LocationService _locationService;
        private readonly EmergencyService _emergencyService;

        [ObservableProperty]
        private string currentStatus = "Safe";

        public string CurrentStatus
        {
            get => currentStatus;
            set => SetProperty(ref currentStatus, value);
        }

        [ObservableProperty]
        private string currentLocation = "Getting location...";

        private bool isTracking;
        public bool IsTracking
        {
            get => isTracking;
            set => SetProperty(ref isTracking, value);
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string Title { get; set; }

        public MainViewModel()
        {
            Title = "Courier Safety";
            _locationService = new LocationService();
            _emergencyService = new EmergencyService();
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await UpdateLocationAsync();
        }

        [RelayCommand]
        private async Task PanicButtonAsync()
        {
            IsBusy = true;
            try
            {
                var result = await _emergencyService.SendPanicAlertAsync("driver123");
                if (result)
                {
                    CurrentStatus = "EMERGENCY - Help is on the way!";
                    await Application.Current.MainPage.DisplayAlert("Alert Sent",
                        "Emergency services have been notified", "OK");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task StartTrackingAsync()
        {
            if (!IsTracking)
            {
                IsTracking = true;
                await _locationService.StartTrackingAsync(location =>
                {
                    CurrentLocation = $"Lat: {location.Latitude:F4}, Lon: {location.Longitude:F4}";
                });
            }
        }

        [RelayCommand]
        private void StopTracking()
        {
            if (IsTracking)
            {
                IsTracking = false;
                _locationService.StopTracking();
                CurrentStatus = "Tracking Stopped";
            }
        }

        private async Task UpdateLocationAsync()
        {
            var location = await _locationService.GetCurrentLocationAsync();
            if (location != null)
            {
                CurrentLocation = $"Lat: {location.Latitude:F4}, Lon: {location.Longitude:F4}";
            }
        }
    }
}

