# Functoso

## Using [LanguageExt](https://github.com/louthy/language-ext) to handle Exceptions and nulls in ASP.NET Core web API

### Nuget Packages

* [LanguageExt.Core](https://github.com/louthy/language-ext)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://docs.fluentvalidation.net/en/latest/#)
* [xUnit](https://xunit.net/)
* [MockHttp](https://github.com/richardszalay/mockhttp)

Data served from [{JSON} Placeholder](https://jsonplaceholder.typicode.com)

Commonly, we model the interaction with the world outside the application with `Task<T>`; things that go in the external (Infrastructure) zone of a [Clean Architecture desing pattern](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html), SQL Connections, HttpClients, File IO. But `Task<T>` is a poor representation of an interaction that can get messy very soon, leaving to the developers to handle nulls, exceptions, etc. [`Aff<T>`](https://github.com/louthy/language-ext/wiki/How-to-deal-with-side-effects) from [LanguageExt](https://github.com/louthy/language-ext) offers an alternative with richier error handling capabilities. The current project is a minimalistic sample of it use.

Reference: [How to deal with side effects](https://github.com/louthy/language-ext/wiki/How-to-deal-with-side-effects)
