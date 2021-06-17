using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class CoordinatorRegisterModel
    {
        public string NID { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string region { get; set; }
        public string visa { get; set; }

    }
}
