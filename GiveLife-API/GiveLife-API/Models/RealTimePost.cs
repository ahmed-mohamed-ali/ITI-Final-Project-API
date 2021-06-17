using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class RealTimePost
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }
        
        public RealTimePost()
        {
            Data = new List<int>();
        }
    }
}
