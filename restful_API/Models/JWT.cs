using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restful_API.Models
{
    public class JWT
    {
        public Guid ID { get; set; }
        public DateTime Datetime { get; set; }
        public string Value { get; set; }


        public string UserLogin { get; set; }
        public User User { get; set; }
    }
}
