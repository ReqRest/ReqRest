namespace ReqRest.Tests
{
    using System;
    using System.Net.Http;
    using Moq;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public class ApiRequestBaseTests : TestBase<ApiRequestBase>
    {

        protected override ApiRequestBase CreateService()
        {
            return CreateService(() => null!, null);
        }

        protected virtual ApiRequestBase CreateService(Func<HttpClient> httpClientProvider, HttpRequestMessage? httpRequestMessage)
        {
            return new Mock<ApiRequestBase>(httpClientProvider, httpRequestMessage) { CallBase = true }.Object;
        }

        public class ConstructorTests : ApiRequestBaseTests
        {

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(Func<HttpClient> httpClientProvider, HttpRequestMessage? httpRequestMessage)
            {
                var ex = Record.Exception(() => _ = CreateService(httpClientProvider, httpRequestMessage));
                var inner = ex.InnerException;
                Assert.IsType<ArgumentNullException>(inner);
            }

            [Fact]
            public void Uses_Specified_HttpClientProvider()
            {
                Func<HttpClient> httpClientProvider = () => null!;
                var service = CreateService(httpClientProvider, null);
                Assert.Same(httpClientProvider, service.HttpClientProvider);
            }

            [Fact]
            public void Uses_Specified_HttpRequestMessage()
            {
                var msg = new HttpRequestMessage();
                var service = CreateService(() => null!, msg);
                Assert.Same(msg, service.HttpRequestMessage);
            }

            [Fact]
            public void Uses_New_HttpRequestMessage_If_Null()
            {
                var service = CreateService(() => null!, null);
                Assert.NotNull(service.HttpRequestMessage);
            }

        }

        public class HttpClientProviderTests : ApiRequestBaseTests
        {

            [Fact]
            public void Throws_ArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Service.HttpClientProvider = null!);
            }

        }

    }

}
