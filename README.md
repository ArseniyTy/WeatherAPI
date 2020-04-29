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

| Method    | URL                                           | Body                      | Description                                                  |
| :-------: | :-------------------------------------------- | :---------                | :----------------------------------------------------------- |
| GET       | http://localhost:44326/users                   | -                         | Get all users from database. For this     endpoint need JWT or it will be empty
| POST      | http://localhost:44326/users/register          | <code>{<br />"firstName": "Jason23",<br />"lastName": "Watmore23",<br />"username": "jason2",<br /> "password": "123qwe"<br />}</code> | Registrate user, but not authenticate
| POST      | http://localhost:44326/users/Authenticate      | <pre lang="json">{<br>"username": "jason",<br>"password": "123qwe"<br>}</pre>| Authenticate user
| POST      | http://localhost:44326/users/Authenticate      | <pre lang="json">{<br>"username": "jason",<br>"password": "123qwe"<br>}</pre>| Authenticate user



| Method    | URL                                           | Body                      | Description                                                  |
| :-------: | :-------------------------------------------- | :---------                | :----------------------------------------------------------- |
| GET       | http://localhost:44326/api/auth/authorization  | <code>{<br/>"value": "yourJWT"<br/>}</code> | returns Ok() if authorization is successful
| POST       | http://localhost:44326/api/auth/authentification | <code>{<br/>"login": "yourLogin",<br/>"password": "yourPassword"<br/>}</code> | returns Ok() if authentification is successful
| POST       | http://localhost:44326/api/auth/creation | <code>{<br/>"login": "yourLogin",<br/>"password": "yourPassword"<br/>}</code> | returns new ObjectResult(user) if succesful
