# it-club.net

1) довести до ума api (catch ошибок)        -yes
2) норм контролеры (get/post/delete/put)       -yes
3) шифрование данных
4) документация вашего api                  -yes

|-------------------------|
|url|params|response|error|
|-------------------------|
|                         |
|-------------------------|




# WeatherForecastController
| Method    | URL                                                | Body       | Description                                                      |
| :-------: | :--------------------------------------------      | :--------- | :-----------------------------------------------------------     |
| GET    | http://localhost:44326/api/weatherforecast/city  || Get a single forecast data by "city" field.
| POST   | http://localhost:44326/api/weatherforecast/city  || Create a forecast by "city" field.
| PUT    | http://localhost:44326/api/weatherforecast/city  || Update a forecast data by "city" field.
| DELETE | http://localhost:44326/api/weatherforecast/city  || Delete forecast by "city" field.
| GET    | http://localhost:44326/api/weatherforecast       || Get all forecasts.
| PATCH  | http://localhost:44326/api/weatherforecast       || Update all forecasts data.



# UserController
| Method    | URL                                           | Body       | Description                                                 |
| :-------: | :-------------------------------------------- | :--------- | :-----------------------------------------------------------|
| GET    | http://localhost:44326/api/user/forecasts   |[JWT model](#json-jwt-model)| Get all user's forecasts by JWT.
| POST   | http://localhost:44326/api/user/addCities   |[JWTWithObject model](#json-jwt-model), where Object is an array of "string" - cities| Add forecasts (by array of cities) to user's forecasts.
| DELETE | http://localhost:44326/api/user/addCities   |[JWTWithObject model](#json-jwt-model), where Object is an array of "string" - cities| Delete forecasts (by array of cities) from user's forecasts.



# AuthController
| Method    | URL                                           | Body       | Description                                                 |
| :-------: | :-------------------------------------------- | :--------- | :-----------------------------------------------------------|
| POST   | http://localhost:44326/api/auth/create        |[User model](#json-user-model)| Create a single user. Login(unique, length: [2;20]) and password(length: [5;100]) are required.
| PUT    | http://localhost:44326/api/auth/update        |[2 users tuple](#json-2-users-tuple)| Update the user by changing property values. Item1 represents OldUser(old login and password are required), and Item2 representes NewUser(all changes are here).
| DELETE | http://localhost:44326/api/auth/delete        |[User model](#json-user-model)| Deletes the user(password and login are required).
| POST   | http://localhost:44326/api/auth/authentificate|[User model](#json-user-model)| Authentificate the user by creating JWT.
| GET    | http://localhost:44326/api/auth/authorize     |[JWT model](#json-jwt-model)| Authorize the user by JWT.




# Controllers bodies:

#### JSON User model:
```json
{
  "Login": "yourLogin",
  "Password": "yourPassword"
}
```
#### JSON 2-users tuple:
```json
{
	"Item1": {
		"Login": "oldLogin",
		"Password": "oldPassword"
	},
	"Item2": {
		"Login": "newLogin",
		"Password": "newPassword"
	}
}
```
#### JSON JWT model:
```json
{
  "Value": "yourJWTValue"
}
```
#### JSON JWTWithObject model:
```json
{
  "JwtValue": "yourJWTValue",
  "Object": [ "Minsk", "Kiev", "London" ]       //here object is an array of "string", but it could be any other one
}
```
