_Please note that this repository's default branch is the `dev` branch. Switch to the `master`
branch to read the description of the current release._

# ReqRest [![Build Status](https://dev.azure.com/ManuelRoemer/ReqRest/_apis/build/status/ReqRest?branchName=master)](https://dev.azure.com/ManuelRoemer/ReqRest/_build/latest?definitionId=12&branchName=master) ![Nuget](https://img.shields.io/nuget/v/ReqRest.Client.svg) ![C# 8.0](https://img.shields.io/badge/C%23-Nullable%20Reference%20Types-success.svg)

A .NET library for creating fully typed wrappers for RESTful APIs with minimal effort.


## What is ReqRest?

At its core, ReqRest allows you to build fully typed REST API Clients.
Let's assume that we want to wrap a REST API which, amongst others, offers the following endpoint.

| Endpoint | Status Code | Response |
| -------- | ----------- | -------- |
| `/users/{id}/todos` | 200 | `{  }`
```

```csharp
var client = new DemoApiClient();

// Make a request to get all Todo resources of the user with the ID 1.
var response = await client.Users(1).Todos().Get().FetchResponseAsync();

// Let's assume that the API returns these types for the following status codes:
//     200: TodoItem[]
// 400-599: ErrorResponse
//  Other : <Undefined>

// ReqRest now allows you to handle all of these possible responses without
// having to know about the status codes, for example like this (there are also other ways):
resource.Match(
    todoItems => Console.WriteLine($"There are {todoItems.Count()} todo items!"),
    error     => Console.WriteLine($"Received an error: {error.Message}."),
    ()        => Console.WriteLine($"Received an entirely different status code.")
);

if (response.TryGetValue(out IEnumerable<TodoItem> todoItems))
{
    Console.WriteLine($"There are {todoItems.Count()} items!");
}

```


## Installation

The library is available on NuGet. Install it via:

```sh
Install-Package ReqRest.Client
Install-Package ReqRest.Serializers.NewtonsoftJson # Optional, but desired in most cases.

--or--

dotnet add package ReqRest.Client
dotnet add package ReqRest.Serializers.NewtonsoftJson # Optional, but desired in most cases. 
```

While `ReqRest.Client` is the main package which you will want to install in 99.9% of cases,
the whole library is split into multiple packages from which you can choose:

| Package Name                         | NuGet Version | Description |
| ------------------------------------ | ------------- |------------ |
| `ReqRest.Client`                     | ![Nuget](https://img.shields.io/nuget/v/ReqRest.Client.svg) | The main package which contains the required members to wrap a REST API. |
| `ReqRest.Builders`                   | ![Nuget](https://img.shields.io/nuget/v/ReqRest.Builders.svg) | Provides builders and builder extension methods which enable fluent configuration of classes like `HttpRequestMessage`. |
| `ReqRest.Http`                       | ![Nuget](https://img.shields.io/nuget/v/ReqRest.Http.svg) | Contains constants and members that are missing from `System.Net.Http`. |
| `ReqRest.Serializers`                | ![Nuget](https://img.shields.io/nuget/v/ReqRest.Serializers.svg) | Provides the base members for serializers that are used by the library. |
| `ReqRest.Serializers.NewtonsoftJson` | ![Nuget](https://img.shields.io/nuget/v/ReqRest.Serializers.NewtonsoftJson.svg) | Provides a JSON (de-)serializer and integration methods for the `ReqRest.Client` package. Uses the `Newtonsoft.Json` for the JSON (de-)serialization. |


## Getting Started

Detailed guides will be written once the library is more or less stable.

For now, have a look at `src/DemoApplication`.
In there, you can find a functional demo app which uses ReqRest to wrap the [JSON Placeholder](https://jsonplaceholder.typicode.com/)
API.
I recommend to look at the following files (in order) and then just follow the comments:

* `Program.cs`
* `JsonPlaceholderClient.cs`
* `TodosInterface.cs`
* `TodoInterface.cs`


## Versioning

As long as the library is still in the initial development (i.e. on version `0.x.x`) any
version increment may signify a breaking change.
Large changes will definitly lead to a minor version increment.
Small breaking changes may only increment the patch number though.

As soon as the library reaches version `1.0.0` it will follow Semantic Versioning.


## Contributing

Any kind of contribution is welcome! Be sure to submit bugs and feature requests via GitHub issues.

Before starting to write code for a (larger) Pull Request, be sure to talk about the changes that
you are going to make, so that we can ensure that the changes are going to have a chance of being 
accepted.


### Git and CI

Development is done in separate branches. Once changes are done, they will be merged into the
`dev` branch.

Once there are enough changes to justify a new library version, `dev` will be merged into `master`
and published.
As a result, `master` always represents the state of the latest release. 


## License

See the [LICENSE](./LICENSE) file for details.
