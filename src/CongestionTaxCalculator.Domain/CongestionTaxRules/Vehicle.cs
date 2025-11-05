using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{
    // ✅ Entity: has identity (e.g., LicensePlate)
    public class Vehicle:Entity
    {
        public string LicensePlate { get; }
        public string Type { get; }

        public Vehicle(string licensePlate, string type)
        {
            LicensePlate = licensePlate;
            Type = type;
        }
    }
}
