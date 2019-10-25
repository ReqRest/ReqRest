
namespace ReqRest.Tests.Sdk.Mocks.Providers
{
    using System;
    using System.Net.Http;
    using ReqRest.Tests.Sdk.Data;

    public sealed class HttpMethodMockProvider : MockDataProvider<HttpMethod>
    {

        public string Method { get; }

        public HttpMethodMockProvider(string method = "GET")
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public override HttpMethod Create() =>
            new HttpMethod(Method);

    }

}
