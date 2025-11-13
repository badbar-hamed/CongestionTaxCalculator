using CongestionTaxCalculator.Domain.Common;
using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Vehicles
{

    public class Vehicle : AggregateRoot<int>
    {
        public VehicleType Type { get; }
        private readonly List<VehiclePass> _passes = new();

        public IReadOnlyCollection<VehiclePass> Passes => _passes.AsReadOnly();
        public string LicensePlate { get; }
        private Vehicle() { }        
        public Vehicle(string licensePlate, VehicleType type)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentException("License plate is required.", nameof(licensePlate));

            LicensePlate = licensePlate;
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public void RegisterPass(DateTime passMoment)
        {
            _passes.Add(new VehiclePass(passMoment));
        }
    }
}
