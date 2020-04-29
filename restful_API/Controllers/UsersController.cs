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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public UsersController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _db.Users.ToArray();
        }

        //чтобы не было ошибки "json cycle detected" нужно в Startup прописать .AddNewtonsoftJson
        [HttpGet]
        public IEnumerable<WeatherForecast> GetUserForecasts([FromBody]UserWeatherJSON user)
        {
            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT.Value == user.JwtValue);
            if (userFromDb == null)
                throw new Exception("No User found");

            var userforecastsFromDb = _db.UserWeatherForecasts
                .Where(uw => uw.UserLogin == userFromDb.Login)
                .Include(uw => uw.WeatherForecast)
                .ToList();
            var forecasts = userforecastsFromDb.Select(f => f.WeatherForecast);

            return forecasts;
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody]User user)
        {
            var userFromDb = _db.Users.FirstOrDefault(u => u.Login == user.Login);
            if (userFromDb != null)
                return BadRequest();

            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }

        [HttpPut]
        public ActionResult UpdateUserCityList([FromBody]UserWeatherJSON userAndWeather)
        {
            var jwtFromDb = _db.JWTs
                .FirstOrDefault(o => o.Value == userAndWeather.JwtValue);
            if (jwtFromDb == null)
                return NotFound();

            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT == jwtFromDb);
            if (userFromDb == null)
                return NotFound();

            var forecastFromDb = _db.WeatherForecasts
                .FirstOrDefault(f => f.City == userAndWeather.CityName);
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
    public class UserWeatherJSON
    {
        public string JwtValue { get; set; }
        public string CityName { get; set; }
    }
}
