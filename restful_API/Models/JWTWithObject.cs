using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restful_API.Models
{
    public class JWTWithObject<T>
    {
        [Required(ErrorMessage = "JWT must have a value")]
        public string JwtValue { get; set; }
        [Required(ErrorMessage = "You must have an \"Object\". Or use just JWT model instead")]
        public T Object { get; set; }
    }
}
