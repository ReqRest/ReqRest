namespace ReqRest.Serializers.Resources
{
    using System;

    internal static class ExceptionStrings
    {

        public static string HttpContentSerializationException_Message() =>
            "The (de-)serialization failed because of an unknown error. See the inner exception for details (if available).";

        public static string HttpContentSerializer_HttpContentIsNullButShouldNotBeNoContent(Type type) =>
            $"Cannot deserialize the type \"{type.FullName}\" because the HTTP response had no content.";

    }

}
