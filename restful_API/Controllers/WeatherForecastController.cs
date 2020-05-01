using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using restful_API.Models;

namespace restful_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ApplicationContext _db;

        public WeatherForecastController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet("{city}")]
        public ActionResult<WeatherForecast> GetForecast(string city)
        {
            var forecastFromDb = _db.WeatherForecasts.FirstOrDefault(f => f.City == city);
            if (forecastFromDb==null)
            {
                return NotFound("There is no such a forecast");
            }
            return new ObjectResult(forecastFromDb);
        }

        [HttpPost("{city}")]
        public ActionResult CreateForecast(string city)
        {
            var cities = _db.WeatherForecasts.Select(o => o.City).ToArray();
            if (cities.Contains(city))
            {
                return BadRequest("This forecast already exists");
            }

            string result = "";
            try
            {
                result = GetDataFromAnotherAPI(city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            JObject o = JObject.Parse(result);  //парсим в JSON объект

            string summary = (string)o["weather"][0]["main"];   //достаём из dictionary информацию
            float temperature = (float)o["main"]["temp"] - (float)273.15;

            var newForecast = new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                City = city,
                Temperature = temperature,
                Summary = summary
            };
                
            _db.WeatherForecasts.Add(newForecast);
            _db.SaveChanges();

            return Ok(newForecast);
        }


        [HttpPut("{city}")]
        public ActionResult<WeatherForecast> UpdateForecast(string city)
        {
            var forecast = _db.WeatherForecasts.FirstOrDefault(f => f.City == city);
            if (forecast == null)
                return NotFound("There is no such a forecast");


            string result = "";
            try
            {
                result = GetDataFromAnotherAPI(city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            JObject o = JObject.Parse(result);

            string summary = (string)o["weather"][0]["main"];
            float temperature = (float)o["main"]["temp"] - (float)273.15;

            forecast.Summary = summary;
            forecast.Temperature = temperature;
            forecast.Date = DateTime.Now;


            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{city}")]
        public ActionResult<WeatherForecast> DeleteForecast(string city)
        {
            var forecastFromDb = _db.WeatherForecasts.FirstOrDefault(f => f.City == city);
            if (forecastFromDb == null)
                return NotFound("There is no such a forecast");

            _db.Remove(forecastFromDb);
            _db.SaveChanges();
            return Ok();
        }




        [HttpGet]
        public IEnumerable<WeatherForecast> GetAllForecasts()
        {
            return _db.WeatherForecasts.ToArray();
        }
        [HttpPatch]
        public ActionResult UpdateAllForecasts()
        {
            var errorMessages= new List<string>();

            foreach (var forecast in _db.WeatherForecasts.ToList())
            {
                string result="";
                try
                {
                    result = GetDataFromAnotherAPI(forecast.City);
                }
                //добавляем в спісок ошібок, который вернём в конце
                catch(Exception ex)
                {
                    errorMessages.Add(forecast.City + "(error occurred): " + ex.Message);
                }
                JObject o = JObject.Parse(result);

                string summary = (string)o["weather"][0]["main"];
                float temperature = (float)o["main"]["temp"] - (float)273.15;

                forecast.Summary = summary;
                forecast.Temperature = temperature;
                forecast.Date = DateTime.Now;
            }
            _db.SaveChanges();
            return Ok(errorMessages);
        }



        //запрос на стороннюю api
        private string API_KEY = "4e935e3c0d5efdaf091fc25cbd0249ea";
        private string GetDataFromAnotherAPI(string city)
        {
            WebRequest request = WebRequest
                .Create($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}"); //посылаем запрос на сторонній сервер

            WebResponse response = request.GetResponse();   //получаем ответ


            var result = new StringBuilder();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))  //чтение из потока
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        result.Append(line);
                    }
                }
            }
            response.Close();

            return result.ToString();
        }
    }
}
