namespace ReqRest.Builders.Resources
{
    internal static class ExceptionStrings
    {

        public static string HttpContentBuilderExtensions_NoHttpContentHeaders() =>
            "Cannot interact with the content headers, because the HttpContent which is being built is null.";

    }

}
