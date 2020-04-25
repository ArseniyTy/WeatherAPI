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
        [HttpGet("{login}")]
        public IEnumerable<WeatherForecast> GetUserForecasts(string login)
        {
            var userforecastsFromDb = _db.UserWeatherForecasts
                .Where(uw => uw.UserLogin == login)
                .Include(uw=>uw.WeatherForecast)
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
        public ActionResult UpdateUserCityList([FromBody]UserWeatherForecast userAndWeather)
        {
            var userFromDb = _db.Users
                .FirstOrDefault(u => u.Login == userAndWeather.User.Login 
                                    && u.Password == userAndWeather.User.Password);
            if (userFromDb == null)
                return BadRequest();

            var forecastFromDb = _db.WeatherForecasts
                .FirstOrDefault(f => f.City == userAndWeather.WeatherForecast.City);
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
