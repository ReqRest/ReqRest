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

    public abstract partial class ApiRequestTestBase<TRequest>
    {

        // The name of the HttpMessageHandler.SendAsync() method.
        private const string SendAsyncMethod = "SendAsync";

        [Fact]
        public async Task FetchResponseAsync_Throws_InvalidOperationException_If_HttpClientProvider_Returns_Null()
        {
            var req = CreateDynamicRequest();
            Func<Task> testCode = async () => await req.FetchResponseAsync();
            await testCode.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task FetchResponseAsync_Uses_HttpClient_To_Make_Request()
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
        public async Task FetchResponseAsync_Sends_HttpRequestMessage_Of_Request()
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

}
