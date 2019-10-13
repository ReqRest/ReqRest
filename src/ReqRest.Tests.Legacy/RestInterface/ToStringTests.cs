namespace ReqRest.Tests.RestInterface
{
    using FluentAssertions;
    using Xunit;

    public class ToStringTests : RestInterfaceTestBase
    {

        [Fact]
        public void Returns_Url_ToString()
        {
            var @interface = CreateInterface(RestClient);
            @interface.ToString().Should().Be(@interface.Url.ToString());
        }

    }

}
