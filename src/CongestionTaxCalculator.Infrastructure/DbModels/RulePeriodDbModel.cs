using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.DbModels
{
    public class RulePeriodDbModel
    {
        public int Id { get; set; }      
        public int RuleId { get; set; }//must be Index
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public decimal Fee { get; set; }


    }
}
