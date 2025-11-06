using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.CalculateTax
{
    public class CalculateTaxRequest
    {
        public string VehiclePlate { get; set; }
        public DateOnly Date { get; set; }
    }
}
