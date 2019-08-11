namespace ReqRest.Client.Tests.RestInterface
{
    using FluentAssertions;
    using ReqRest.Client;
    using Xunit;

    public class UrlTests : RestInterfaceTestBase
    {

        [Fact]
        public void Url_Returns_Uri_Built_Via_GetUrlBuilder()
        {
            var @interface = CreateInterface(RestClient);
            var builtUrl = ((IUrlProvider)@interface).GetUrlBuilder().Uri;
            @interface.Url.Should().Be(builtUrl);
        }

    }

}
