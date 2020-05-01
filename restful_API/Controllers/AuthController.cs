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

        [HttpPost("create")]
        public ActionResult CreateUser([FromBody]User user)
        {
            var userFromDb = _db.Users.FirstOrDefault(u => u.Login == user.Login);
            if (userFromDb != null)
                return BadRequest("The user with such a login currently exists");


            user.PasswordSalt = SecurityService.GetSalt();
            user.Password = SecurityService.GetHash(user.Password + user.PasswordSalt);

            _db.Users.Add(user);
            _db.SaveChanges();
            return new ObjectResult(user);
        }

        [HttpPut("update")]
        public ActionResult UpdateUser([FromBody]Tuple<User,User> users)
        {
            var oldUser = users.Item1;
            var newUser = users.Item2;


            var userFromDb = _db.Users.
                FirstOrDefault(u => u.Login == oldUser.Login);
            if (userFromDb == null)
                return NotFound("There is no such a user");
            if (userFromDb.Password != SecurityService.GetHash(oldUser.Password + userFromDb.PasswordSalt))
                return new ForbidResult("Password is incorrect");


            if(newUser.Login!=null)
                userFromDb.Login = newUser.Login;
            if (newUser.Password != null)
                userFromDb.Password = newUser.Password;

            _db.SaveChanges();
            return new ObjectResult(userFromDb);
        }

        [HttpDelete("delete")]
        public ActionResult DeleteUser([FromBody]User user)
        {
            var userFromDb = _db.Users.
                FirstOrDefault(u => u.Login == user.Login);
            if (userFromDb == null)
                return NotFound("There is no such a user");
            if (userFromDb.Password != SecurityService.GetHash(user.Password + userFromDb.PasswordSalt))
                return new ForbidResult("Password is incorrect");


            _db.Users.Remove(userFromDb);
            _db.SaveChanges();
            return Ok();
        }





        [HttpPost("authentificate")]
        public ActionResult AuthentificateUser([FromBody]User user)
        {
            var userFromDb = _db.Users.
                FirstOrDefault(u => u.Login == user.Login);
            if (userFromDb == null)
                return NotFound("There is no such a user");
            if (userFromDb.Password != SecurityService.GetHash(user.Password + userFromDb.PasswordSalt))
                return new ForbidResult("Password is incorrect");

            //удаляем старую jwt, еслі она есть
            var oldJWT = _db.JWTs.FirstOrDefault(j => j.UserLogin == userFromDb.Login);
            if (oldJWT != null)
                _db.JWTs.Remove(oldJWT);


            //добавляем новую
            var id = Guid.NewGuid();
            var datetime = DateTime.Now;
            var jwt = new JWT
            {
                ID = id,
                UserLogin = userFromDb.Login,
                Datetime = DateTime.Now,
                Value = SecurityService.GetHash(id.ToString() + datetime.ToString() + userFromDb.Login)
            };
            _db.JWTs.Add(jwt);
            userFromDb.JWT = jwt;

            _db.SaveChanges();
            return Ok(jwt.Value);
        }

        [HttpGet("authorize")]
        public ActionResult AuthorizeUser([FromBody]JWT jwt)
        {
            var jwtFromDb = _db.JWTs.FirstOrDefault(u => u.Value == jwt.Value);
            if (jwtFromDb == null)
                return NotFound("There is no such a JWT");

            //а разве нам нужно так делать, сервер же не храніт данные о пользователях. он же берёт всё із jwt
            var userFromDb = _db.Users.FirstOrDefault(u => u.JWT == jwtFromDb);
            if (userFromDb == null)
                return NotFound("This JWT doesn't belong to anybody");

            return Ok(userFromDb);
        }




        // в POST переделать authentificate ??? - позыріть когда какой  і как в целом настроіть работу JWT токена 
    }
}