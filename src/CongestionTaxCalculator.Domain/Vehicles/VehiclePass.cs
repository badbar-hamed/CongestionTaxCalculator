using CongestionTaxCalculator.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Vehicles
{
    
    public class VehiclePass:Entity<int>
    {        
        public DateTime PassMoment { get; }

        public VehiclePass( DateTime timestamp)
        {
            PassMoment = timestamp;
        }
    }
}
