namespace ReqRest.Tests.RestInterface
{
    using FluentAssertions;
    using Xunit;

    public class BuildRequestTests : RestInterfaceTestBase
    {

        [Fact]
        public void Returns_New_ApiRequest_Every_Time()
        {
            var @interface = CreateInterface(RestClient);
            var req1 = @interface.BuildRequest();
            var req2 = @interface.BuildRequest();
            req1.Should().NotBeSameAs(req2);
        }

        [Fact]
        public void Request_Uses_HttpClientProvider_From_RestClient_Config()
        {
            var @interface = CreateInterface(RestClient);
            var req = @interface.BuildRequest();
            req.HttpClientProvider.Should().BeSameAs(@interface.Client.Configuration.HttpClientProvider);
        }

        [Fact]
        public void Request_Uses_Url_Of_Interface()
        {
            var @interface = CreateInterface(RestClient);
            var req = @interface.BuildRequest();
            req.HttpRequestMessage.RequestUri.Should().Be(@interface.Url);
        }

    }

}
