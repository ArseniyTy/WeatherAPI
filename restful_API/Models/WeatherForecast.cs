using restful_API.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace restful_API.Models
{
    public class WeatherForecast : IWeatherForecast
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public float Temperature { get; set; }
        public string Summary { get; set; }


        public virtual IList<UserWeatherForecast> UserWeatherForecasts { get; set; }
        public WeatherForecast()
        {
            UserWeatherForecasts = new List<UserWeatherForecast>();
        }
    }
}
