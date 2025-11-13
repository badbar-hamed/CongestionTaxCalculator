using CongestionTaxCalculator.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.DbModels
{
    public class ExemptVehicleTypeDbModel
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }            
        public int RuleId { get; set; }//index

    }
}
