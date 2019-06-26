namespace ReqRest.Builders.Tests
{
    using System.Net.Http;

    public static class HttpClients
    {

        public static HttpClient Default { get; } = new HttpClient();
        public static HttpClient NonDefault { get; } = new HttpClient();

    }

}
