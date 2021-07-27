# SomeAspNetProject
sample asp.net core project with swagger &amp;&amp; mongo, using common practices. 
For checkout please run {project}/Deploy/run.ps1(.sh).
 Then go to http://localhost:1337/swagger/index.html

Here I've used: 

    - Dependency injection (Constructor injection)
    - CQRS via Mediator pattern (MediatR)
    - CRUD with MongoDb
    - Caching with Redis
    - Fluent validation for validating request models
    - Middleware to handle runtime and validation exceptions
    - Requests are parametrized and extended with queryable filtering
    - Unit tests are done with Xunit

Plans:
 - Adding one more controller: Sellers and integrating it with Records (reference by id)
 - Adding auth using OAuth 2.0