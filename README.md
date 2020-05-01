# it-club.net

1) довести до ума api (catch ошибок)
2) норм контролеры (get/post/delete/put)
3) шифрование данных
4) документация вашего api

|-------------------------|
|url|params|response|error|
|-------------------------|
|                         |
|-------------------------|




# WeatherForecastController
| Method    | URL                                                | Body       | Description                                                      |
| :-------: | :--------------------------------------------      | :--------- | :-----------------------------------------------------------     |
| GET         | http://localhost:44326/api/weatherforecast/city  || Get a single forecast data by "city" field.
| POST        | http://localhost:44326/api/weatherforecast/city  || Create a forecast by "city" field.
| PUT         | http://localhost:44326/api/weatherforecast/city  || Update a forecast data by "city" field.
| DELETE      | http://localhost:44326/api/weatherforecast/city  || Delete forecast by "city" field.
| GET         | http://localhost:44326/api/weatherforecast       || Get all forecasts.
| PATCH       | http://localhost:44326/api/weatherforecast       || Update all forecasts data.

| POST       | http://localhost:44326/api/weatherforecast        |          | Create forecasts by JSON array of "cities".


# AuthController
| Method    | URL                                           | Body       | Description                                                 |
| :-------: | :-------------------------------------------- | :--------- | :-----------------------------------------------------------|
| POST      | http://localhost:44326/api/auth/create        |[User model](#json-user-model)| Create a single user. Login(unique, length: [2;20]) and password(length: [5;100]) are required.
| PUT       | http://localhost:44326/api/auth/update        |[2 users tuple](#json-2-users-tuple)| Update the user by changing property values. Item1 represents OldUser(old login and password are required), and Item2 representes NewUser(all changes are here).
| DELETE    | http://localhost:44326/api/auth/delete        |[User model](#json-user-model)| Deletes the user(password and login are required).
| POST      | http://localhost:44326/api/auth/authentificate|[User model](#json-user-model)| Authentificate the user by creating JWT.
| GET      | http://localhost:44326/api/auth/authorize      |[JWT model](#json-jwt-model)| Authorize the user by JWT.



#### JSON User model:
```json
{
  "login": "yourLogin",
  "password": "yourPassword"
}
```
#### JSON 2-users tuple:
```json
{
	"Item1": {
		"login": "oldLogin",
		"password": "oldPassword"
	},
	"Item2": {
		"login": "newLogin",
		"password": "newPassword"
	}
}
```
#### JSON JWT model:
```json
{
  "value": "yourJWTValue"
}
```
