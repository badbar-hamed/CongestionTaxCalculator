using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{

    public class Vehicle : Entity
    {
        public string LicensePlate { get; }
        public VehicleType Type { get; }

        public Vehicle(string licensePlate, string type)
        {
            LicensePlate = licensePlate;
            if (Enum.TryParse(type, out VehicleType parsedVehicleType))
            {
                Type = parsedVehicleType;
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type", nameof(type));
            }

        }
    }
}
