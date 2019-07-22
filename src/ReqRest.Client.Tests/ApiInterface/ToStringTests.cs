namespace ReqRest.Client.Tests.ApiInterface
{
    using FluentAssertions;
    using Xunit;

    public class ToStringTests : ApiInterfaceTestBase
    {

        [Fact]
        public void Returns_Url_ToString()
        {
            var @interface = CreateInterface(DefaultApiClient);
            @interface.ToString().Should().Be(@interface.Url.ToString());
        }

    }

}
