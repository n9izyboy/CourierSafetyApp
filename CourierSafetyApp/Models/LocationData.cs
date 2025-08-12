using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSafetyApp.Models
{
    internal class LocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public string DriverId { get; set; }
        public bool IsInDanger { get; set; }

    }

}
