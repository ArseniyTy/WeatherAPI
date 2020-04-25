using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restful_API.Models.Interfaces;

namespace restful_API.Models
{
    public class User : IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual IList<UserWeatherForecast> UserWeatherForecasts { get; set; }

        public User()
        {
            UserWeatherForecasts = new List<UserWeatherForecast>();
        }
    }
}
