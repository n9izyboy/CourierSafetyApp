using CourierSafetyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSafetyApp.Services
{
    public class EmergencyService
    {
        private readonly LocationService _locationService;

        public EmergencyService()
        {
            _locationService = new LocationService();
        }

        public async Task<bool> SendPanicAlertAsync(string driverId)
        {
            try
            {
                var location = await _locationService.GetCurrentLocationAsync();

                var alert = new EmergencyAlert
                {
                    Id = Guid.NewGuid().ToString(),
                    DriverId = driverId,
                    Location = new LocationData
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Timestamp = DateTime.Now,
                        DriverId = driverId,
                        IsInDanger = true
                    },
                    AlertTime = DateTime.Now,
                    Type = AlertType.PanicButton,
                    Description = "Emergency panic button activated"
                };

                // TODO: Send to API
                await SendAlertToServerAsync(alert);

                // Vibrate device
                Vibration.Vibrate(TimeSpan.FromSeconds(1));

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error sending panic alert: {ex.Message}");
                return false;
            }
        }

        private async Task SendAlertToServerAsync(EmergencyAlert alert)
        {
            // TODO: Implement API call
            // For now, just simulate sending
            await Task.Delay(100);
            System.Diagnostics.Debug.WriteLine($"Alert sent: {alert.Description}");
        }
    }
}

    

