<h1 align="center">
  <img src="./assets/Logo.svg" alt="ReqRest Logo" width="128" height="128" />
</h1>
<h3 align="center">
  Build REST API Wrappers with Ease
</h3>

<div align="center">
    
[![Build Status](https://dev.azure.com/ManuelRoemer/ReqRest/_apis/build/status/ReqRest?branchName=master)](https://dev.azure.com/ManuelRoemer/ReqRest/_build/latest?definitionId=12&branchName=master) ![Azure DevOps tests](https://img.shields.io/azure-devops/tests/ManuelRoemer/ReqRest/18) ![Nuget](https://img.shields.io/nuget/v/ReqRest.svg) ![C# 8.0](https://img.shields.io/badge/C%23-Nullable%20Reference%20Types-success.svg)

[Getting Started](https://reqrest.github.io/articles/getting-started.html) &nbsp; | &nbsp; [Guides](https://reqrest.github.io/articles/guides/index.html) &nbsp; | &nbsp; [API Documentation](https://reqrest.github.io/api/) &nbsp; | &nbsp; [Changelog](./CHANGELOG.md) &nbsp; | &nbsp; [NuGet](https://www.nuget.org/packages/ReqRest/)

> **Note:** This repository's default branch is the `dev` branch. Switch to `master` for the latest release.

</div>

# What is ReqRest? 

ReqRest allows you to easily turn a RESTful Web API into a **fully typed** C# library by using a **declarative, fluent**
syntax. API clients written with ReqRest have full **IntelliSense** and **compiler support** and thus make your REST API
feel like plain C#. 

![Basic Client Usage](./.github/img/BasicClientUsage.gif)

> An example API call demonstrating ReqRest's IntelliSense support.


# Features

### :heavy_check_mark: Declarative, Fluent API

ReqRest makes it incredibly easy to declare how your API behaves for specific requests. Even special
API endpoints can easily be wrapped with ReqRest. 

For example, the fictional API endpoint `GET /todos?page={number}&pageSize={number}` endpoint returns
a `List<TodoItem>` for the status code `200` and an `ErrorMessage` for status codes between `400`
and `599`. It can be declared like this:

```csharp
public ApiRequest<List<TodoItem>, ErrorMessage> Get(int? page, int? pageSize) =>
    BuildRequest()
        .Get()
        .ConfigureRequestUri(url => 
            url & ("page", $"{page}") & ("pageSize", $"{pageSize}")
        )
        .Receive<List<TodoItem>>().AsJson(forStatusCodes: 200)
        .Receive<ErrorMessage>().AsJson(forStatusCodes: (400, 599));
```

### :heavy_check_mark: Convenient API Response Handling

When interacting with a REST API, dealing with status code is a required, but tedious.
ReqRest tackles this and makes your life easier by abstracting status codes away for you.

For example, the code in the section above declares that the API returns a `List<TodoItem>` or an
`ErrorMessage`, depending on the status code.
Because ReqRest now has all these information, it can do the matching for you when interacting with
the response:

```csharp
var (response, resource) = await client.Todos().Get(page: 1, pageSize: 50).FetchAsync();

// Match(...) is only one of many ways to get the actual object that you are interested in.
// Other possible methods are, for example: GetValue, TryGetValue, GetValueOr, ...
resource.Match(
    todoItems => Console.WriteLine($"There are {todoItems.Count} items!"),
    errorMsg  => Console.WriteLine($"There was an error: {errorMsg.Message}."),
    ()        => Console.WriteLine($"Received an unexpected status code: {response.StatusCode}")
);
```

### :heavy_check_mark: Easy to Extend

Nearly every part of ReqRest can be extended depending on your needs. As a matter of fact, ReqRest's
code base mainly consists of extension methods. If we can extend it this way, so can you!

Say that your API offers the special `CUSTOM` HTTP Method - you can easily add such functionality to ReqRest:

```csharp
public static T Custom<T>(this T builder) where T : IHttpMethodBuilder =>
    builder.SetMethod(new HttpMethod("CUSTOM"));

// Use it like this:
BuildRequest()
    .Custom()
    .Receive<Foo>().AsJson(forStatusCode: 200);
```

### :heavy_check_mark: It's Just .NET

One of ReqRest's core philosophies is to simply be a wrapper around .NET's `HttpClient` API.
Anything that you can do with an `HttpClient` is also possible with ReqRest.

For example, the `ApiRequest` class is nothing else but a decorator around .NET's `HttpRequestMessage`
class. As a result, you have full access to the underlying properties. No magic, no nonsense.

### :heavy_check_mark: C# 8.0 Support

ReqRest supports Nullable Reference Types throughout the whole library.

### :heavy_check_mark: Fully Documented

Every public method of ReqRest is extensively documented via XML comments.

# Installation

The library is available on NuGet. Install it via:

```sh
Install-Package ReqRest
Install-Package ReqRest.Serializers.NewtonsoftJson # Optional, but desired in most cases.

--or--

dotnet add package ReqRest
dotnet add package ReqRest.Serializers.NewtonsoftJson # Optional, but desired in most cases. 
```

Depending on your needs, you may want to install additional packages which enhance the base
functionality of ReqRest. In most cases, you will have to install an extra package for JSON support.
These packages are not shipped with ReqRest by default, because they depend on external libraries
or are not compatible with .NET Standard 2.0.

| Package Name                         | NuGet Version | Description |
| ------------------------------------ | ------------- |------------ |
| `ReqRest`                            | ![Nuget](https://img.shields.io/nuget/v/ReqRest.svg) | The main package which contains ReqRest's most important members. |
| `ReqRest.Serializers.NewtonsoftJson` | ![Nuget](https://img.shields.io/nuget/v/ReqRest.Serializers.NewtonsoftJson.svg) | Enhances ReqRest with JSON support using the `Newtonsoft.Json` library. |


# Getting Started

There are multiple ways for getting started with ReqRest.

You should definitely check out the [Getting Started Guide](https://reqrest.github.io/articles/getting-started.html)
in the official documentation.

Afterwards, you can have a look at the [Guides](https://reqrest.github.io/articles/guides/index.html).
In there, you will find in depth tutorials and explanations regarding the usage of ReqRest.

Last but not least, you can always have a look at the [API Documentation](https://reqrest.github.io/api/)
both online and via Visual Studio's IntelliSense. I spend a lot of time on the XML documentation -
it is definitely worth your time to have a look into it.


# Documentation

The public documentation is available at [https://reqrest.github.io](https://reqrest.github.io).

This documentation is generated as part of the build process and stored in a separate repository
available at [https://github.com/ReqRest/ReqRest-Documentation](https://github.com/ReqRest/ReqRest-Documentation).
If you are interested in updating the documentation, be sure to have a look into that repository.


# Contributing

Any kind of contribution is welcome! 
If you have found a bug or have a question or feature request, be sure to
[open an issue](https://github.com/ReqRest/ReqRest/issues/new). Alternatively, depending on the
problem, you can create a new Pull Request and manually make your desired changes.

> :warning: If you intend to make any (larger) changes to the code, be sure to open an issue to talk
> about what you are going to do before starting your work, so that we can ensure that the changes
> are going to have a chance of actually being accepted.

For development, C# 8.0 is used. As a result, you will have to download the .NET Core 3.0 SDK and
at least Visual Studio 2019.

When writing code for the library, please be sure to:

* Follow the established code style of the existing code.
* Always write XML documentation for public members.
* Create test cases for new code (there may be exceptions for this point, but view it as a guideline).


# Versioning

As long as the library is still in the initial development (i.e. on version `0.x.x`) any
version increment may signify a breaking change.
Large changes will definitly lead to a minor version increment.
Small breaking changes may only increment the patch number though.

As soon as the library reaches version `1.0.0` it will follow Semantic Versioning.


# Git and CI

The repository's main branch is the `dev` branch. On this branch, changes are accumulated until
the release of a new version is justified. Once that happens, the current state of `dev` will be
merged into `master` and then automatically be built and deployed to NuGet. 
As a result, `master` will always represent the state of the latest release.


# License

See the [LICENSE](./LICENSE) file for details.
