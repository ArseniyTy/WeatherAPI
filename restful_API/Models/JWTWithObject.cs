using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restful_API.Models
{
    public class JWTWithObject<T>
    {
        public string JwtValue { get; set; }
        public T Object { get; set; }
    }
}
