using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class CaseRegisterModel
    {
        public string Name { get; set; }
        public string NationalId { get; set; }
        public int? FamilyMemberNum { get; set; }
        public int? ChildNum { get; set; }
        public int RegionId { get; set; }
    }
}
