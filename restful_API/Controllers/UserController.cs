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
        [HttpGet("forecasts")]
        public ActionResult<IEnumerable<WeatherForecast>> GetUserForecasts([FromBody]JWT jwt)
        {
            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT.Value == jwt.Value);
            if (userFromDb == null)
                return NotFound("No such JWT");

            var userforecastsFromDb = _db.UserWeatherForecasts
                .Where(uw => uw.UserLogin == userFromDb.Login)
                .Include(uw => uw.WeatherForecast)
                .ToList();
            var forecasts = userforecastsFromDb.Select(f => f.WeatherForecast);

            return new ObjectResult(forecasts);
        }


        [HttpPost("addCities")]
        public ActionResult AddCitiesToUser([FromBody]JWTWithObject<List<string>> JWTWithCityNames)
        {
            var jwtFromDb = _db.JWTs
                .FirstOrDefault(o => o.Value == JWTWithCityNames.JwtValue);
            if (jwtFromDb == null)
                return NotFound("No such JWT");

            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT == jwtFromDb);
            if (userFromDb == null)
                return NotFound("No such JWT");

            //подгружаем его прогнозы для нахожденія повторок
            _db.Entry(userFromDb).Collection(u => u.UserWeatherForecasts).Load();

            //для каждого города із переданного спіска
            foreach (var city in JWTWithCityNames.Object)
            {
                var forecastFromDb = _db.WeatherForecasts
                .FirstOrDefault(f => f.City == city);
                //еслі данного города нет, то нічего не делаем
                if (forecastFromDb == null)
                    continue;

                //еслі данный город уже есть в спіске у юзера, то нічего не делаем
                if (userFromDb.UserWeatherForecasts.
                    FirstOrDefault(uwf => uwf.WeatherForecastId == forecastFromDb.Id) != null)
                    continue;

                //іначе добавляем прогноз к юзеру
                var uwf = new UserWeatherForecast
                {
                    UserLogin = userFromDb.Login,
                    WeatherForecastId = forecastFromDb.Id,
                };
                userFromDb.UserWeatherForecasts.Add(uwf);
                forecastFromDb.UserWeatherForecasts.Add(uwf);
            }
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("deleteCities")]
        public ActionResult DeleteCitiesOfUser([FromBody]JWTWithObject<List<string>> JWTWithCityNames)
        {
            var jwtFromDb = _db.JWTs
                .FirstOrDefault(o => o.Value == JWTWithCityNames.JwtValue);
            if (jwtFromDb == null)
                return NotFound("No such JWT");

            var userFromDb = _db.Users
                .FirstOrDefault(o => o.JWT == jwtFromDb);
            if (userFromDb == null)
                return NotFound("No such JWT");

            //подгружаем его прогнозы для нахожденія повторок
            _db.Entry(userFromDb).Collection(u => u.UserWeatherForecasts).Load();

            //для каждого города із переданного спіска
            foreach (var city in JWTWithCityNames.Object)
            {
                var forecastFromDb = _db.WeatherForecasts
                .FirstOrDefault(f => f.City == city);
                //еслі данного города нет в прогнозах, то нічего не делаем
                if (forecastFromDb == null)
                    continue;

                var uwf = userFromDb.UserWeatherForecasts.
                    FirstOrDefault(uwf => uwf.WeatherForecastId == forecastFromDb.Id);
                //еслі данного города нет в спіске у юзера, то нічего не делаем
                if (uwf == null)
                    continue;

                _db.UserWeatherForecasts.Remove(uwf);
            }
            _db.SaveChanges();
            return Ok();

        }

    }

}
