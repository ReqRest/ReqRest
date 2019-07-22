# Changelog

## v0.3.0

* **[Breaking]** Removed HttpClientProvider in the configuration and replaced it with a `Func<HttpClient>` for simplicity.
* **[Breaking]** The `ApiRequestBase` and derived classes now expect a `Func<HttpClient>` aswell. No `HttpClient` gets passed around directly.
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
