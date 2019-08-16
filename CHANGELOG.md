# Changelog

## v0.4.1

* **[Package]** Fixed the `ReqRest` package description being wrongly formatted.
* Added the `PostJson`, `PutJson` and `PatchJson` methods in the `Newtonsoft` package.
* Fixed a potential NullReferenceException in the `AppendPath` extension method.
* Updated the XML documentation.


## v0.4.0

_This is an update which refactors a lot of things that proved to be wrongly designed or named.
At this point, the library is ~95%+ tested, meaning that it is ready for additional extensions in future updates._

* **[Breaking / Package]** The `ReqRest.Client` package is being renamed/moved to `ReqRest`. As a result, `ReqRest.Client` is now **deprecated**!  
* **[Breaking]** The constructor of the `ApiRequest<T>` (not the `ApiRequest` though) classes are no longer `public`, so that upgrading via `Receive` is enforced.
* **[Breaking]** The constructor of all `ApiResponse` classes are no longer `public`, meaning that instances can only be retrieved by creating an `ApiRequest` first.
* **[Breaking]** Refactored the `ApiResponseBase.CurrentResponseTypeInfo` property to be a method called `GetCurrentResponseTypeInfo`. This fixes a potential bug that the property holds an old value when the status code mutates.
* **[Breaking]** Renamed `ApiClient` to `RestClient`.
* **[Breaking]** Renamed `ApiClientConfiguration` to `RestClientConfiguration`.
* **[Breaking]** Renamed `ApiInterface` to `RestInterface`.
* **[Breaking]** The `ApiResponseBase.StatusCode` property is now of type `HttpStatusCode`, not of type `Int32`.
* **[Breaking]** Removed the `StatusCodeRange.IsInRange(Int32)` method.
* The `HttpRequestMessageBuilder` and `IHttpResponseMessageBuilder` (and thus, the `ApiRequest` and `ApiResponse` classes) now publicly expose the wrapped properties of the underlying HTTP message class, so that interacting with `ApiRequest` and `ApiResponse` instances becomes more used friendly.
* The `StatusCodeRange` now supports negative numbers.
* The `IHttpContentDeserializer` and related members now allow deserializing an `HttpContent` which is `null`. This previously threw an `ArgumentNullException` which didn't make sense when attempting to deserialize `NoContent`. This behavior has been updated.
* Changed the `ApiRequestBase.PossibleResponseTypes` from an `IEnumerable` to an `IReadOnlyCollection`.
* Changed the `ApiResponseBase.PossibleResponseTypes` from an `IEnumerable` to an `IReadOnlyCollection`.
* Updated the XML documentation.


## v0.3.0

* **[Breaking]** Removed HttpClientProvider in the configuration and replaced it with a `Func<HttpClient>` for simplicity.
* **[Breaking]** The `ApiRequestBase` and derived classes now expect a `Func<HttpClient>` as well. No `HttpClient` gets passed around directly.
* **[Breaking]** The `ApiRequestBase.SetHttpClient` method was renamed to `SetHttpClientProvider` and now accepts a `Func<HttpClient>` (in addition to an `HttpClient`).
* **[Breaking]** `ApiResponseBase.CurrentResponseTypeInfo` is no longer virtual.
* **[Breaking]** `ApiClient<TConfig>` now has the `where TConfig : new()` constraint.
* `ApiClient` now accepts `null` as configuration parameter. It uses a newly created, default configuration instance in this case.
* The `ApiInterface`'s inherited methods (e.g. `ToString` and `GetHashCode`) are now hidden from IntelliSense, so that consumers of such an API can focus on the relevant members.
* Added additional, pre-defined status code ranges like `Informational` or `ClientErrors` to `StatusCodeRange`.
* XML documentation updates and fixes.
* On another note, the library is now mostly covered by unit tests.


## v0.2.5

* **[Breaking]** Made `NoContent` a struct and removed the `Default` property.
* Added a custom `UrlBuilder`, extending the `System.UriBuilder` with convenience operators.
* Added `IHttpHeadersBuilder.SetHeader` methods.
* Added a `IHttpMethodBuilder.Patch` method.
* Added `AppendPath` and `AppendQueryParameter` methods.
* Added `ToString` overrides in various classes for debugging convenience.
* Added `ReceiveNoContent` method in `ApiRequest` variations.
* XML documentation updates and fixes.


## v0.2.4

* Added and integrated the special `NoContent` type into/to the serializers. 


## v0.2.3

* **[Breaking]** Removed `SetJsonFormContent`.
* Fixed the `UriBuilder` being created with the wrong `Uri` in the `ApiClient`.


## v0.2.2

* Added `SetFormUrlEncodedContent` methods.


## v0.2.1

* Strong named the assemblies.


## v0.2.0

* **[Breaking]** Updated the namespaces to match the project names. The `ReqRest` namespace is no longer shared.
* **[Breaking]** Renamed `ReqRest.Api` to `ReqRest.Client`.
* Added new helper methods to every `IBuilder` (`If`, `IfNot`, `Configure`).


## v0.1.0

* Initial release of the library on NuGet.
