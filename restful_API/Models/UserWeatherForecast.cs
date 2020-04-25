using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restful_API.Models
{
    public class UserWeatherForecast
    {
        public string UserLogin { get; set; }
        public User User { get; set; }

        public Guid WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
    }
}
