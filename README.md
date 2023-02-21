# market-data-gateway

I have been quite short on time:
* could only implement adding a contribution, did not get to Get or List.
* uses an in-memory validation service and repository

Unfortunately there is a bug: the Json payload does not seem to be deserialised correctly and I am not sure why - I do not have time to investigate further. Also the MarketDataType is serialised as a number and not as a string; also did not had time to look up the option for this.

Testing and comments are basic - though that does not show here due to time constraints I tend to document the public classes I create. I wanted to show some examples of tests (e.g., acceptance, using mocks...) but it would need much more than that.

That said I wanted to demonstrate:
* some understanding of AspNet
* development practices around tests
* layered architecture

Some further notes:

## WebApi

The WebApi part is not that polished, especially around error handling. In my current company we have a base webapi template, common utilities and helpers (I actually contributed a lot to that!) and some established patterns on how to represent failures, etc... (NB: we use LanguageExt, a C# functional library that gives us monads like Either<F, T>). But here, with having to stay with the base webapi machinery, I had no time to look the documentation and polish things.

It would require more validation attributes on the external DTO to cleanly reject invalid ones with a nice error message - and also a lot more testing around that.

Also for failures/errors I should return a nice ProblemDetail response - but that can be tedious to write so I skipped it.

I am missing tests that roundtrip objects between DTO and model objects to make sure I got the mapping and serialisation right.

Also missing is a health endpoint, that can be useful when deployed on Kubernetes or other system that uses it to detect when application is ready.

Requests logging could be done through some custom middlewares (I did work on that in my company as well, sending logs to kafka)

Very obviously, I am missing:
* get a contribution
* delete a contribution
* update
* list all contributions - ideally listing should support pagination, filtering, sorting...

## Model

The model is not perfect, but it probably shows that I like "immutable classes" with static factory methods - validation can be enforced in these static factory methods.

I only support FxQuotes, but wanted to come with something that could be extended to other type of quotes that could have very different parameters - whence the QuoteBase class etc...

I initially used a Guid for QuoteId, but I thought "what if a user send the same quote twice - or worse, conflicting quotes? How would I identify this?". I decided to use the Currency pair + quote timestamp as the identifier - we should have only one quote for a specific type for a given currency pair.

It felt like there is little business logic so my objects are not very rich. I encapsulated the "Validate and store" logic in a domain service - this could have been commands; alternatively we could have put that in the MarketDataContribution object itself, but I found that less "nice" than the domain service option.

My MarketDataContrbution etc... are also likely missing a version field (but also I am not dealing with deletes and updates)

I am not dealing with Unit of Work and transactions.

In my current company we do not use Entity Framework or other libraries that could help deal with some of technical aspects of the domain model - so I decided not to go that route; learning it would have taken me more time and I was very limited.

In the Model.Tests project, there is an abstract test class for the contribution repository, with test cases that all implementation should pass.

A real implementation would probably require some audit, etc... so we may have metadata about the user that made the request, the time of the request, etc...

Everything is a class. I know about records but we do not use them yet in my current job - I decided to stick with what I am familiar with.

## Infrastructure

The stub MarketDataValidation service only validates whether a pair is on some allowed list; I wanted to use that to be able to test both positive and negative case (though I did not had time to write an acceptance test for the negative case). An alternative could have been to rely on Http headers (the validation service could use a HttpContextAccessor to get it) to indicate whether the validation is successful or not - this can be handy during testing vs. using magic strings and values.

The in memory repository is very simplistic - it uses 2 maps, one of which is used to index contribution by their quoteId (I want to detect when people submit a duplicate quote). Concurrent users should be supported due to the use concurrent dictionaries (but I did not had time to write a test for it). It will over fail if we add updates or deletes as this will open the door to race conditions - more complex locking will be required.

I did not go for SQL implementation due to lack of time. Besides, my current work has custom libraries and patterns for dealing with Database. With limited time, I did not feel like digging through the documentation to try to make things work in a vanilla environment.

## Monitoring

Had to skip this, but I would integrate with Prometheus.NET library to provide basic runtime and aspnet metrics. Custom API metrics (nb calls, errors...) could be implemented with a middleware - but the base monitoring provided by Prometheus.Net Aspnet adapter may be enough.

A monitoring principle is to monitor the edge of the service, so I would add some wrappers, etc... around the validation service and the repository to keep track of the traditional RED metrics for these: nb calls, nb errors, latency histogram/summaries. Depending on their implementation, a real validation service/repository could have some more metrics, closer to the edge (i.e., making external calls).

Had no time to consider logging and to configure it/inject ILogger<> everywhere. Errors/exceptions should of course be logged at all level. We also probably want to log incoming requests and their results, possibly with details about the request body and the response. We also likely want the Id of the calls to the validation service. This would depend on what level of audit is required.

## Performance

I like to have strongly typed objects, especially for Ids etc... (instead of having functions taking 5 strings parameters). This will create some "garbage" - we could possibly improve by using struct.

Avenues for performance testing would be BenchmarkDotNet (which also includes memory/GC profiling). For load testing with concurrent users, I've had some good experience with K6.

## Other

This has been implemented as a synchronous request/response model. Using an asynchronous model (queues) may require some changes. We could want to persist contribution immediately in a "NON-VALIDATED" state before transitioning them. THis has not been considered here.