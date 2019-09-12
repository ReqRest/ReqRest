# Changelog

## v0.5.0

_This release tackles another bad decision - splitting up the library into multiple small packages.
It turned out that this provided next to no benefit and will thus be changed while ReqRest is still in development mode.
From now on, there will be one main package (`ReqRest`) and several supportive packages which either have external
dependencies or are simply not compatible with .NET Standard 2.0, e.g. the `ReqRest.Serializers.NewtonsoftJson` package._

* **[Breaking / Package]** The multiple `ReqRest.*` packages have been consolidated into the single `ReqRest` package. The namespaces haven't changed, but the old packages are deprecated from this release on.
* **[Breaking]** Moved the `NoContent` type from the `ReqRest.Serializers` namespace to `ReqRest.Http`.


## v0.4.2

* **[Breaking]** Updated the `StatusCodeRange.ToString()` method to return strings similar to `[-200, 300]`, so that negative status codes look better (before, strings like `-300--200` were possible).
* **[Breaking]** Updated the `ResponseTypeInfo.ToString()` method to respect the new status code range string format.
* **[Internal / Breaking]** The internally used `NoContentSerializer` now throws a `NotSupportedException` instead of an `InvalidOperationException` when an unsupported method gets called by the user. This is an uncommon scenario that is not expected to happen in common usage of the library.
* Added the `ApiRequest.FetchAsync` methods which combine `FetchResponseAsync` and `FetchResourceAsync` by returning both the response and a deserialized resource for development convenience.
* Added the `ApiRequest.ReceiveString` and `ApiRequest.ReceiveByteArray` methods.
* Added the `RequestUriBuilderExtensions.ConfigureRequestUri(Func<UrlBuilder, Uri?>)` method to support more fluent URI configuration (from `builder.ConfigureRequestUri(url => url = url & ("foo", "bar"))` to `builder.ConfigureRequestsUri(url => url & ("foo", "bar"))`.
* The `UrlBuilder` can now be implicitly converted to an `Uri`.
* Fixed several nullable annotations dealing with setting or serializing an `HttpContent`. This mainly concerns the `HttpContentSerializer` and `SetHttpContent` method.
* The `HttpContentSerializer` now returns `null` when serializing `NoContent`.
* Updated the XML documentation, specifically of the builder interfaces and the `ApiRequest` and `ApiResponse` classes.

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
