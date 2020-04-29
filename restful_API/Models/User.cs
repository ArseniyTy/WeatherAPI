using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using restful_API.Models.Interfaces;

namespace restful_API.Models
{
    [DataContract]
    public class User : IUser
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }

        public virtual IList<UserWeatherForecast> UserWeatherForecasts { get; set; }
        public JWT JWT { get; set; }

        public User()
        {
            UserWeatherForecasts = new List<UserWeatherForecast>();
        }
    }
}
