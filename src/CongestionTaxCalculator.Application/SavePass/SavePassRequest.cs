using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.SavePass
{
    public class SavePassRequest
    {
        public string VehiclePlate { get; set; }

        public IEnumerable<DateTime> VehiclePassDates { get; set; }
    }
}
