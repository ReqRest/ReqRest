namespace ReqRest.Tests.RestInterface
{
    using FluentAssertions;
    using Moq;
    using ReqRest.Builders;
    using Xunit;

    public class GetUrlBuilderTests : RestInterfaceTestBase
    {

        [Fact]
        public void Returns_UrlBuilder_Of_BaseUrlProvider()
        {
            var builder = new UrlBuilder();
            var baseUrlProviderMock = new Mock<IUrlProvider>();
            var @interface = CreateInterface(RestClient, baseUrlProviderMock.Object);
            baseUrlProviderMock.Setup(x => x.GetUrlBuilder()).Returns(builder);

            ((IUrlProvider)@interface).GetUrlBuilder().Should().BeSameAs(builder);
        }

        [Fact]
        public void Calls_BuildUrl()
        {
            bool wasCalled = false;
            var @interface = CreateInterface(RestClient, buildUrl: BuildUrl);
            ((IUrlProvider)@interface).GetUrlBuilder();
            wasCalled.Should().BeTrue();

            UrlBuilder BuildUrl(UrlBuilder baseUrl)
            {
                wasCalled = true;
                return baseUrl;
            }
        }

        [Fact]
        public void Calls_BuildUrl_With_UrlBuilder_Of_BaseUrlProvider()
        {
            var builder = new UrlBuilder();
            var baseUrlProviderMock = new Mock<IUrlProvider>();
            var @interface = CreateInterface(RestClient, baseUrlProviderMock.Object, BuildUrl);
            baseUrlProviderMock.Setup(x => x.GetUrlBuilder()).Returns(builder);
            ((IUrlProvider)@interface).GetUrlBuilder();

            UrlBuilder BuildUrl(UrlBuilder baseUrl)
            {
                baseUrl.Should().BeSameAs(builder);
                return baseUrl;
            }
        }

    }

}
