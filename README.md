# AspNet Store:
"Store" is an API project that emulates the computer details accouning system for some web store. It was done using ASP.NET Core with swagger &amp;&amp; MongoDb, Redis, and using common practices.
For checkout please run {project.git.dir}/Deploy/run.ps1(.sh).

Then go to:
http://localhost:1488/swagger/index.html - auth host; roles, users and login controllers - Use ' DefaultAdmin / Admin_12345' to login

http://localhost:1337/swagger/index.html - internal host; sellers and records controllers

Here I've used: 

    - Auth via JWT with required actions for each endpoint
    - Dependency injection (constructor injection) for services
    - CQRS via Mediator pattern (MediatR)
    - CRUD with MongoDb
    - Migrations with js and migrate-mongo package
    - Caching and blacklisting with Redis
    - Fluent validation for validating requests models
    - Middleware to handle runtime and validation exceptions
    - Requests are parametrized and extended with queryable filtering
    - Unit tests are done with Xunit, Moq, Fluent Validations, Fluent Assertions

Plans:

External host:
- Client controller: buy some things (records)
- Moving from linq sorts to MongoDb built-in queries
