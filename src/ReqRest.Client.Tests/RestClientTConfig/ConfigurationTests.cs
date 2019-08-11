namespace ReqRest.Client.Tests.RestClientTConfig
{
    using System;
    using FluentAssertions;
    using Moq;
    using ReqRest.Client;
    using Xunit;

    public class ConfigurationTests
    {

        [Fact]
        public void Throws_InvalidCastException_If_Types_Dont_Match()
        {
            var config = new RestClientConfiguration();
            var client = CreateClient();
            ((RestClient)client).Configuration = config;

            Action testCode = () => _ = client.Configuration;
            testCode.Should().Throw<InvalidCastException>();
        }
        
        [Fact]
        public void Returns_Configuration_Of_Base_Class()
        {
            var config = new CustomRestClientConfiguration();
            var client = CreateClient();
            ((RestClient)client).Configuration = config;
            client.Configuration.Should().BeSameAs(config);
        }

        [Fact]
        public void Sets_Configuration_Of_Base_Class()
        {
            var config = new CustomRestClientConfiguration();
            var client = CreateClient();
            client.Configuration = config;
            ((RestClient)client).Configuration.Should().BeSameAs(config);
        }

        protected RestClient<CustomRestClientConfiguration> CreateClient(CustomRestClientConfiguration config = null)
        {
            return new Mock<RestClient<CustomRestClientConfiguration>>(config).Object;
        }

    }

}
