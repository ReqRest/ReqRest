namespace ReqRest.Tests.RestClient
{
    using FluentAssertions;
    using Moq;
    using ReqRest;
    using Xunit;

    public class ConstructorTests
    {

        [Fact]
        public void Uses_Specified_Configuration()
        {
            var config = new RestClientConfiguration();
            var client = new Mock<RestClient>(config).Object;
            client.Configuration.Should().BeSameAs(config);
        }

        [Fact]
        public void Creates_Default_Configuration_If_None_Is_Specified()
        {
            var client = new Mock<RestClient>(null).Object;
            client.Configuration.Should().NotBeNull().And.BeOfType<RestClientConfiguration>();
        }

    }

}
