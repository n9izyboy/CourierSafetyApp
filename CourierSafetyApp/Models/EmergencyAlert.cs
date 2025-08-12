using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSafetyApp.Models
{
    internal class EmergencyAlert
    {
        public string Id { get; set; }
        public string DriverId { get; set; }
        public LocationData Location { get; set; }
        public DateTime AlertTime { get; set; }
        public AlertType Type { get; set; }
        public string Description { get; set; }
    }
    public enum AlertType
    {
        PanicButton,
        SuspiciousBehavior,
        VehicleIssue,
        CrimeZoneEntry
    }
}
