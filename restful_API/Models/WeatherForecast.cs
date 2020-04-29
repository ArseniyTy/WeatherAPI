using restful_API.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace restful_API.Models
{
    [DataContract]
    public class WeatherForecast : IWeatherForecast
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public float Temperature { get; set; }
        [DataMember]
        public string Summary { get; set; }


        public virtual IList<UserWeatherForecast> UserWeatherForecasts { get; set; }
        public WeatherForecast()
        {
            UserWeatherForecasts = new List<UserWeatherForecast>();
        }
    }
}
