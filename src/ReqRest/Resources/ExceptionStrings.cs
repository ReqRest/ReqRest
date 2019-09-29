namespace ReqRest.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Http;

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
            "The IHttpContentDeserializer factory for the current response type returned null " +
            "instead of a deserializer instance.";

        public static string ApiResponse_NoResponseTypeInfoForResponse() =>
            $"The response had a status code which was not configured. " +
            $"When building a request via the ApiRequest API, ensure that you declare a possible " +
            $"response for this status code via the \"{nameof(ApiRequest.Receive)}\" methods, " +
            $"so that a response to the request knows which type to deserialize for the given status code.";

        public static string HttpClientProvider_Returned_Null() =>
            "The configured HttpClient provider function returned null. " +
            "Ensure that the function returns a valid HttpClient instance.";

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

        public static string Serializer_CanOnlyDeserialize(Type type) =>
            $"The serializer can only deserialize objects of type {type.FullName}.";

        #endregion

        #region ReqRest.Builders

        public static string HttpContentBuilderExtensions_NoHttpContentHeaders() =>
            "Cannot interact with the content headers, because the HttpContent which is being built is null.";

        #endregion

        #region ReqRest.Http

        public static string StatusCodeRange_FromCannotBeGreaterThanTo() =>
            $"Cannot create a status code range where {nameof(StatusCodeRange.From)} is greater " +
            $"than {nameof(StatusCodeRange.To)}.";

        #endregion

        #region ReqRest.Serializers

        public static string HttpContentSerializationException_Message() =>
            "The (de-)serialization failed because of an unknown error. See the inner exception for details (if available).";

        public static string HttpContentSerializer_ContentTypeDoesNotMatchActualType(Type expected, Type actual) =>
            $"The content to be serialized does not match the specified type. " +
            $"Expected an instance of the class ${expected.FullName}, but got {actual.FullName}.";

        public static string HttpContentSerializer_HttpContentIsNullButShouldNotBeNoContent(Type type) =>
            $"Cannot deserialize the type \"{type.FullName}\" because the HTTP response had no content.";

        #endregion

    }

}
