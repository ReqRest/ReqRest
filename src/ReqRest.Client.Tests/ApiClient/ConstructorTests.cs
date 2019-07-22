namespace ReqRest.Client.Tests.ApiClient
{
    using FluentAssertions;
    using Moq;
    using ReqRest.Client;
    using Xunit;

    public class ConstructorTests
    {

        [Fact]
        public void Uses_Specified_Configuration()
        {
            var config = new ApiClientConfiguration();
            var client = new Mock<ApiClient>(config).Object;
            client.Configuration.Should().BeSameAs(config);
        }

        [Fact]
        public void Creates_Default_Configuration_If_None_Is_Specified()
        {
            var client = new Mock<ApiClient>(null).Object;
            client.Configuration.Should().NotBeNull().And.BeOfType<ApiClientConfiguration>();
        }

    }

}
