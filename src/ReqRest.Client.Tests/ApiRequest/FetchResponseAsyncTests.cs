namespace ReqRest.Client.Tests.ApiRequest
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Moq;
    using Moq.Protected;
    using ReqRest.Client;
    using Xunit;

    public abstract class FetchResponseAsyncTestBase<TRequest> : ApiRequestTestBase<TRequest>
        where TRequest : ApiRequestBase
    {

        // The name of the HttpMessageHandler.SendAsync() method.
        private const string SendAsyncMethod = "SendAsync";

        [Fact]
        public async Task Throws_InvalidOperationException_If_HttpClientProvider_Returns_Null()
        {
            var req = CreateDynamicRequest();
            Func<Task> testCode = async () => await req.FetchResponseAsync();
            await testCode.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Uses_HttpClient_To_Make_Request()
        {
            var (httpMessageHandlerMock, httpClient) = MockHttpClientAndHandler();
            var req = CreateDynamicRequest(() => httpClient);
            await req.FetchResponseAsync();

            // The only thing that we care about is that the HttpClient was used to send
            // the request message.
            httpMessageHandlerMock.Protected().Verify(
                SendAsyncMethod,
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task Sends_HttpRequestMessage_Of_Request()
        {
            var (httpMessageHandlerMock, httpClient) = MockHttpClientAndHandler();
            var req = CreateDynamicRequest(() => httpClient);
            await req.FetchResponseAsync();

            httpMessageHandlerMock.Protected().Verify(
                SendAsyncMethod,
                Times.Once(),
                req.HttpRequestMessage,
                ItExpr.IsAny<CancellationToken>()
            );
        }

        private (Mock<HttpMessageHandler>, HttpClient) MockHttpClientAndHandler()
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    SendAsyncMethod,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage());

            return (httpMessageHandlerMock, httpClient);
        }

    }

    public class ApiRequestFetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest>
    {
        protected override ApiRequest CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.Create(httpClientProvider);
    }

    public class ApiRequestT1FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1>>
    {
        protected override ApiRequest<Dto1> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT1(httpClientProvider);
    }

    public class ApiRequestT2FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2>>
    {
        protected override ApiRequest<Dto1, Dto2> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT2(httpClientProvider);
    }

    public class ApiRequestT3FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2, Dto3>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT3(httpClientProvider);
    }

    public class ApiRequestT4FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT4(httpClientProvider);
    }

    public class ApiRequestT5FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT5(httpClientProvider);
    }

    public class ApiRequestT6FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT6(httpClientProvider);
    }

    public class ApiRequestT7FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT7(httpClientProvider);
    }

    public class ApiRequestT8FetchResponseAsyncTests : FetchResponseAsyncTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7, Dto8>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7, Dto8> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT8(httpClientProvider);
    }

}
