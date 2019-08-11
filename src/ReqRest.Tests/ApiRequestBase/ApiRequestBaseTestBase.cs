namespace ReqRest.Tests.ApiRequestBase
{
    using System;
    using System.Net.Http;
    using Moq;
    using ReqRest;

    public abstract class ApiRequestBaseTestBase
    {

        protected Func<HttpClient> DefaultHttpClientProvider { get; } = () => null;

        protected ApiRequestBase CreateRequest(
            Func<HttpClient> httpClientProvider, 
            HttpRequestMessage httpRequestMessage = null)
        {
            var mock = new Mock<ApiRequestBase>(httpClientProvider, httpRequestMessage);
            mock.CallBase = true;
            return mock.Object;
        }

    }

}
