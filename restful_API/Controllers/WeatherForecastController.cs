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
            
        [HttpGet]
        public IEnumerable<WeatherForecast> GetAll()
        {
            return _db.WeatherForecasts.ToArray();
        }

        [HttpPost]
        public ActionResult CreateForecast([FromBody]WeatherForecast data)
        {
            var cities = _db.WeatherForecasts.Select(o => o.City).ToArray();
            if (cities.Contains(data.City))
            {
                return BadRequest("This forecast already exists");
            }

            string result = "";
            try
            {
                result = GetDataFromAnotherAPI(data.City);
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
                City = data.City,
                Temperature = temperature,
                Summary = summary
            };
                
            _db.WeatherForecasts.Add(newForecast);
            _db.SaveChanges();

            return Ok(newForecast);
        }

        //обновление данных всех городов
        [HttpPatch]
        public ActionResult UpdateForecastsData()
        {
            foreach (var forecast in _db.WeatherForecasts.ToList())
            {
                string result="";
                try
                {
                    result = GetDataFromAnotherAPI(forecast.City);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                JObject o = JObject.Parse(result);

                string summary = (string)o["weather"][0]["main"];
                float temperature = (float)o["main"]["temp"] - (float)273.15;

                forecast.Summary = summary;
                forecast.Temperature = temperature;
                forecast.Date = DateTime.Now;
            }
            _db.SaveChanges();
            return Ok();
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


        //DELETE - удаленіе прогнозов
    }
}
