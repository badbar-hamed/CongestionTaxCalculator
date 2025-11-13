using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.DbModels
{
    public class VehicleDbModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public virtual VehicleTypeDbModel Type { get; set; }

        public string LicensePlate { get; set; }

        public virtual ICollection<PassDbModel> Passes { get; set; }
    }
}
