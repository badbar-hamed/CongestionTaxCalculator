using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.DbModels
{
    public class PassDbModel
    {      
        public int VehicleId { get; set; }//index
        public DateTime Moment { get; set; }

        
    }
}
