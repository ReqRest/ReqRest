namespace ReqRest.Client.Tests.ApiInterface
{
    using FluentAssertions;
    using Xunit;

    public class BuildRequestTests : ApiInterfaceTestBase
    {

        [Fact]
        public void Returns_New_ApiRequest_Every_Time()
        {
            var @interface = CreateInterface(DefaultApiClient);
            var req1 = @interface.BuildRequest();
            var req2 = @interface.BuildRequest();
            req1.Should().NotBeSameAs(req2);
        }

        [Fact]
        public void Request_Uses_HttpClientProvider_From_ApiClient_Config()
        {
            var @interface = CreateInterface(DefaultApiClient);
            var req = @interface.BuildRequest();
            req.HttpClientProvider.Should().BeSameAs(@interface.Client.Configuration.HttpClientProvider);
        }

        [Fact]
        public void Request_Uses_Url_Of_Interface()
        {
            var @interface = CreateInterface(DefaultApiClient);
            var req = @interface.BuildRequest();
            req.HttpRequestMessage.RequestUri.Should().Be(@interface.Url);
        }

    }

}
