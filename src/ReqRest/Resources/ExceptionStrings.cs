namespace ReqRest.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Builders;
    using ReqRest.Http;
    using ReqRest.Serializers;

    /// <summary>
    ///     Provides static methods which return exception message strings for the various exceptions
    ///     thrown in the library.
    ///
    ///     These could, in theory, be retrieved from a resource file, but I am against localizing
    ///     exception messages. Hence, they can just be returned from plain functions.
    /// </summary>
    internal static class ExceptionStrings
    {

        #region ReqRest

        public static string ApiResponse_InvalidResponseDeserializer() =>
            $"The {nameof(IHttpContentDeserializer)} factory for the current response type returned " +
            $"null (Nothing in VB) instead of an actual deserializer instance.";

        public static string ApiResponse_NoResponseTypeInfoForResponse() =>
            $"The response had a status code which has not been declared before. " +
            $"When building a request via the {nameof(ApiRequest)} API, ensure that you declare a possible " +
            $"response for this status code via the \"{nameof(ApiRequest.Receive)}\" methods, " +
            $"so that a response to the request knows which type should be deserialized for the given status code.";

        public static string HttpClientProvider_Returned_Null() =>
            $"The configured HttpClient provider function returned null (Nothing in VB). " +
            $"If you are seeing this exception while using a {nameof(RestClient)}, you can most " +
            $"likely fix this error by setting the {nameof(RestClientConfiguration)}.{nameof(RestClientConfiguration.HttpClientProvider)} " +
            $"property to a function which returns an actual HttpClient instance.";

        public static string ResponseTypeInfo_MustProvideAtLeastOneStatusCode() =>
            "At least one status code or status code range must be provided.";

        public static string ResponseTypeInfoCollection_ConflictingStatusCodeRanges(
            IEnumerable<(StatusCodeRange, StatusCodeRange)> conflictingStatusCodes)
        {
            var conflictingStr = string.Join(
                "\n",
                conflictingStatusCodes.Select(pair => $"- {pair.Item1} and {pair.Item2}")
            );

            return 
                $"Some status code ranges have conflicting values. " +
                $"When defining the possible response types of an API request, ensure that the status " +
                $"code ranges don't cover the same status codes (except in combination with wildcards).\n\n" +
                $"The conflicting status codes are:\n" +
                $"{conflictingStr}";
        }

        #endregion

        #region ReqRest.Internal

        public static string SpecificHttpContentSerializer_CanOnlyDeserialize(Type type) =>
            $"The serializer can only deserialize objects of type \"{type.FullName}\".";

        #endregion

        #region ReqRest.Builders

        public static string HttpContentHeaders_HttpContent_Is_Null() =>
            $"The headers of the HttpContent cannot be configured, because the HttpContent is " +
            $"null (Nothing in VB). Ensure that the {nameof(IHttpContentBuilder.Content)} property " +
            $"is set to an actual HttpContent instance.";

        #endregion

        #region ReqRest.Http

        public static string StatusCodeRange_FromCannotBeGreaterThanTo() =>
            $"Cannot create a status code range where {nameof(StatusCodeRange.From)} is greater " +
            $"than {nameof(StatusCodeRange.To)}.";

        #endregion

        #region ReqRest.Serializers

        public static string HttpContentSerializationException_Message() =>
            "The (de-)serialization failed because of an arbitrary error. This most likely happened, " +
            "because an inner serializer failed to (de-)serialize the given data. " +
            "See the inner exception for details (if available).";

        public static string HttpContentSerializer_ContentTypeDoesNotMatchActualType(Type expected, Type actual) =>
            $"The content to be serialized does not match the specified type. " +
            $"Expected an instance of the class \"${expected.FullName}\", but got \"{actual.FullName}\".";

        public static string HttpContentSerializer_HttpContentIsNull(Type contentType) =>
            $"The HttpContent to be deserialized into an object of type \"{contentType.FullName}\" " +
            $"was null (Nothing in VB). An HttpContent which is null cannot be deserialized by this " +
            $"serializer.\n" +
            $"If you know that the HttpContent is always going to be null, ensure that you deserialize " +
            $"the special \"{typeof(NoContent).FullName}\" type. This type can always be deserialized, even " +
            $"if no HttpContent is given.";

        #endregion

    }

}
