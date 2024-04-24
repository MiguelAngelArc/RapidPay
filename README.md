# How to run the project

The easiest way to run this project is to install **docker** and ***docker compose***, then only run the command:
```bash
docker compose up -d
```

You can also run the project on a local machine but if you do so, then you will need to create the required database tables that are located under the folder
***sql_server_setup***, then run the script ***setup.sql*** to create the tables and a single user. Finally edit the ***appsettings.json*** file to point to the correct database

In this project I create endpoints for user management but also a default user with the following credentials already exists
```
Username = "miguel.arcos@encora.com"
Password = "P455w0rd"
```
There is a controller to create ***JWT*** tokens for this user (There is a example in the postman collection), but also this controller can create new users and then link the cards to users.

All the operations that involve card management need a ***JWT*** to work, so, first you need to create the ***JWT*** and then pass this ***JWT*** to Card Management requests in the **Authorization Header** (Theorically every time you run a ***sign-in*** or ***sign-up*** in postman the token is stored automatically in the collection variables).

If you run the project using ***docker compose*** then the URL is this ***http://localhost:5001***, if you run it on your local machine the ***launchSettings.json*** file is configured to use the following URLs ***https://localhost:8001;http://localhost:8000*** (One for ***https*** and one for ***http***)

Note: Every time you do a SignUp the token generated overwrites your token in the collection variables leading to possible errors like ***"card-does-not-belong-to-user"*** if you use the same CardId with this new token
