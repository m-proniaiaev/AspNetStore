# AspNet Store:
Sample asp.net core project with swagger &amp;&amp; MongoDb, Redis, using common practices.
For checkout please run {project.git.dir}/Deploy/run.ps1(.sh).

Then go to:
http://localhost:1337/swagger/index.html - internal host; sellers and records controllers

Here I've used: 

    - Dependency injection (constructor injection)
    - CQRS via Mediator pattern (MediatR)
    - CRUD with MongoDb
    - Caching with Redis
    - Fluent validation for validating request models
    - Middleware to handle runtime and validation exceptions
    - Requests are parametrized and extended with queryable filtering
    - Unit tests are done with Xunit, Moq, Fluent Validations, Fluent Assertions

Plans:

Auth host:
 - User controller
 - Adding auth using OAuth 2.0

External host:
- Client controller
