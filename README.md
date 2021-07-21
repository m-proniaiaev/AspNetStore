# SomeAspNetProject
sample asp.net core project with swagger &amp;&amp; mongo, using common practices. 

Here I've used: 

    - Dependency injection (Constructor injection)
    - MediatR for handling api requests
    - Work with MongoDb (connection string is stored in user-secrets since db is cloud-hosted)
    - Fluent validation for checking commands 
    - Middleware to handle exceptions
    - Unit tests with Xunit

Plans:
 - Adding one more controller: Sellers and integrating it with Records (reference by id)
 - Extend request parametrs by adding queryable filtering
 - Docker support
