using CongestionTaxCalculator.Domain.Common;
using CongestionTaxCalculator.Domain.CongestionTaxRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.DbModels
{
    public class RuleDbModel
    {
        public int Id { get; set; }
        public decimal DailyCap { get; set; }
        public int SingleChargePeriodMinutes { get; set; }
        public ICollection<RulePeriodDbModel> Periods { get; set; }
        public ICollection<ExemptVehicleTypeDbModel> ExemptVehicles { get; set; }
    }
}
