# SomeAspNetProject
sample asp.net core project with swagger &amp;&amp; mongo, using common practices. 
For checkout please run {project}/Deploy/run.ps1(.sh).
 Then go to http://localhost:1337/swagger/index.html

Here I've used: 

    - Dependency injection (Constructor injection)
    - MediatR pattern for handling async api requests
    - Work with MongoDb
    - Fluent validation for checking request commands 
    - Middleware to handle runtime and validation exceptions
    - Request are parametrized with queryable filtering
    - Unit tests with Xunit

Plans:
 - Adding one more controller: Sellers and integrating it with Records (reference by id)
 - Adding auth using OAuth 2.0
 - Caching requests
