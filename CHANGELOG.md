# Changelog

## v0.x.x

* Added additional, pre-defined status code ranges like `Informational` or `ClientErrors` to `StatusCodeRange`.


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
