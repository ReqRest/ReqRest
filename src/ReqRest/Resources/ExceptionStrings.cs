namespace ReqRest.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using ReqRest.Http;

    internal static class ExceptionStrings
    {

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

    }

}
