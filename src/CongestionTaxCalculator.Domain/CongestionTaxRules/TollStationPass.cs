using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.CongestionTaxRules
{
    // ✅ Entity: unique per (Vehicle + Timestamp)
    public class TollStationPass:Entity
    {
        public Vehicle Vehicle { get; }
        public DateTime Timestamp { get; }

        public TollStationPass(Vehicle vehicle, DateTime timestamp)
        {
            Vehicle = vehicle;
            Timestamp = timestamp;
        }
    }
}
