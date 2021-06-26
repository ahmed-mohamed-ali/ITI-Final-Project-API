using GiveLifeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiveLife_API.Models
{
    public class PublishPost
    {
        public string PostMessage { get; set; }
        public decimal RequiredAmount { get; set; }
        public String NeedCatogry { get; set; }
        public string CaseNationalId { get; set; }

    }
}
