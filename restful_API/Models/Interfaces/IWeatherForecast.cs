using System;

namespace restful_API.Models.Interfaces
{
    public interface IWeatherForecast
    {
        Guid Id { get; set; }
        DateTime Date { get; set; }
        string City { get; set; }
        float Temperature { get; set; }
        string Summary { get; set; }
    }
}
