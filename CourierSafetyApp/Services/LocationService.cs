using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSafetyApp.Services
{
    public class LocationService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isTracking;

        public async Task<Location> GetCurrentLocationAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status == PermissionStatus.Granted)
                {
                    var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(10)
                    });

                    return location;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                System.Diagnostics.Debug.WriteLine($"Unable to get location: {ex.Message}");
            }
            return null;
        }

        public async Task StartTrackingAsync(Action<Location> onLocationChanged)
        {
            _isTracking = true;
            _cancelTokenSource = new CancellationTokenSource();

            await Task.Run(async () =>
            {
                while (_isTracking && !_cancelTokenSource.Token.IsCancellationRequested)
                {
                    var location = await GetCurrentLocationAsync();
                    if (location != null)
                    {
                        Device.BeginInvokeOnMainThread(() => onLocationChanged(location));
                    }
                    await Task.Delay(5000); // Update every 5 seconds
                }
            }, _cancelTokenSource.Token);
        }

        public void StopTracking()
        {
            _isTracking = false;
            _cancelTokenSource?.Cancel();
        }
    }
}
    

