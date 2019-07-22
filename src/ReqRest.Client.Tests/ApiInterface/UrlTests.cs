namespace ReqRest.Client.Tests.ApiInterface
{
    using FluentAssertions;
    using ReqRest.Client;
    using Xunit;

    public class UrlTests : ApiInterfaceTestBase
    {

        [Fact]
        public void Url_Returns_Uri_Built_Via_GetUrlBuilder()
        {
            var @interface = CreateInterface(DefaultApiClient);
            var builtUrl = ((IUrlProvider)@interface).GetUrlBuilder().Uri;
            @interface.Url.Should().Be(builtUrl);
        }

    }

}
