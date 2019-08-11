namespace ReqRest.Client.Tests.RestClient
{
    using System;
    using FluentAssertions;
    using Moq;
    using ReqRest.Client;
    using Xunit;

    public class ConfigurationTests
    {

        [Fact]
        public void Throws_ArgumentNullException()
        {
            var client = new Mock<RestClient>(null).Object;
            Action testCode = () => client.Configuration = null;
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Sets_Configuration()
        {
            var config = new RestClientConfiguration();
            var client = new Mock<RestClient>(null).Object;
            client.Configuration = config;
            client.Configuration.Should().BeSameAs(config);
        }

    }

}
