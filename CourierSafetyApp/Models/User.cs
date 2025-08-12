using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierSafetyApp.Models
{
    internal class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string VehicleId { get; set; }
        public string CompanyId { get; set; }
    }
    public enum UserType
    {
        Driver,
        Client,
        CompanyAdmin
    }
}
