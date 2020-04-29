using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using restful_API.Models;

namespace restful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public AuthController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody]User user)
        {
            var userFromDb = _db.Users.
                FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (userFromDb == null)
                return BadRequest();

            var id = Guid.NewGuid();
            var datetime = DateTime.Now;
            var jwt = new JWT
            {
                ID = id,
                UserLogin = userFromDb.Login,
                Datetime = DateTime.Now,
                Value = id.ToString() + datetime.ToString()
            };

            _db.JWTs.Add(jwt);
            userFromDb.JWT = jwt;

            _db.SaveChanges();
            return Ok(jwt.Value);
        }

        [HttpGet]
        public ActionResult CreateUser([FromBody]JWT jwt)
        {
            var jwtFromDb = _db.JWTs.FirstOrDefault(u => u.Value == jwt.Value);

            if (jwtFromDb == null)
                return BadRequest();

            var userFromDb = _db.Users.FirstOrDefault(u => u.JWT == jwtFromDb);

            if (userFromDb == null)
                return BadRequest(new ClientErrorData());

            return Ok(userFromDb);
        }
    }
}