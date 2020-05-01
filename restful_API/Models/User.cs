﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using restful_API.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace restful_API.Models
{
    [DataContract]
    public class User : IUser
    {
        [Required(ErrorMessage = "User must have a login")]
        [StringLength(maximumLength: 20, MinimumLength = 2, 
            ErrorMessage = "Login length should be in diaposon [2;20]")]
        [DataMember]
        public string Login { get; set; }
        

        [Required(ErrorMessage = "User must have a password")]
        [StringLength(maximumLength: 100, MinimumLength = 5,
            ErrorMessage = "Password length should be in diaposon [5;100]")]
        [DataMember]
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public virtual IList<UserWeatherForecast> UserWeatherForecasts { get; set; }
        public JWT JWT { get; set; }

        public User()
        {
            UserWeatherForecasts = new List<UserWeatherForecast>();
        }
    }
}
