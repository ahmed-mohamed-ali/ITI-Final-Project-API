using GiveLifeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class CreateCuponModel
    {
        public int CuponId { get; set; }
        public string CuponIdentity { get; set; }
        public decimal AmountOfMoney { get; set; }
        public string CaseNationalId { get; set; }
        public int? CoordId { get; set; }
        public string ProductCategory { get; set; }
        public int RegionId { get; set; }

    }
}
