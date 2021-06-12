using GiveLifeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class Donnation
    {
        public int DonnationID { get; set; }
        public decimal DonnationAmout { get; set; }
        public int? RegionCoordinatorId { get; set; }
        public virtual RegionCoordinator RegionCoordinator { get; set; }
        public int? OnlineDonnerId { get; set; }
        public virtual OnlineDonner OnlineDonner { get; set; }
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

    }
}
