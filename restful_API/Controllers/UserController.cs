using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restful_API.Models;

namespace restful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public UserController(ApplicationContext context)
        {
            _db = context;
        }

        //чтобы не было ошибки "json cycle detected" нужно в Startup прописать .AddNewtonsoftJson
        [HttpGet]
        public IEnumerable<WeatherForecast> GetUserForecasts([FromBody]JWT jwt)
        {
            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT.Value == jwt.Value);
            if (userFromDb == null)
                throw new Exception("No User found");

            var userforecastsFromDb = _db.UserWeatherForecasts
                .Where(uw => uw.UserLogin == userFromDb.Login)
                .Include(uw => uw.WeatherForecast)
                .ToList();
            var forecasts = userforecastsFromDb.Select(f => f.WeatherForecast);

            return forecasts;
        }


        //сделать массів городов, проход і добавленіе каждогго
        //мб добавіть, чтобы /addcity, а не просто put
        [HttpPut]
        public ActionResult UpdateUserCityList([FromBody]JWTWithObject<string> jwtWithCityName)
        {
            var jwtFromDb = _db.JWTs
                .FirstOrDefault(o => o.Value == jwtWithCityName.JwtValue);
            if (jwtFromDb == null)
                return NotFound();

            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT == jwtFromDb);
            if (userFromDb == null)
                return NotFound();

            var forecastFromDb = _db.WeatherForecasts
                .FirstOrDefault(f => f.City == jwtWithCityName.Object);
            if (forecastFromDb == null)
                return BadRequest();


            var uwf = new UserWeatherForecast
            {
                UserLogin = userFromDb.Login,
                WeatherForecastId = forecastFromDb.Id,
            };
            userFromDb.UserWeatherForecasts.Add(uwf);
            forecastFromDb.UserWeatherForecasts.Add(uwf);
            _db.SaveChanges();
            return Ok();
        }
    }
}
