using GiveLifeAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class MoneyTransformation
    {
        
        public int id { get; set; }
        public int RegionAdminId { get; set; }
        public int RegionCoordinatorId { get; set; }
        public decimal MoneyAmount { get; set; }
        public virtual RegionAdmin regionAdmin{ get; set; }
        public virtual RegionCoordinator RegionCoordinator { get; set; }
    }
}
