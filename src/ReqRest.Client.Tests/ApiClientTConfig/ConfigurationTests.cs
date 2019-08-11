namespace ReqRest.Client.Tests.ApiClientTConfig
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
            var config = new ApiClientConfiguration();
            var client = CreateClient();
            ((ApiClient)client).Configuration = config;

            Action testCode = () => _ = client.Configuration;
            testCode.Should().Throw<InvalidCastException>();
        }
        
        [Fact]
        public void Returns_Configuration_Of_Base_Class()
        {
            var config = new CustomApiClientConfiguration();
            var client = CreateClient();
            ((ApiClient)client).Configuration = config;
            client.Configuration.Should().BeSameAs(config);
        }

        [Fact]
        public void Sets_Configuration_Of_Base_Class()
        {
            var config = new CustomApiClientConfiguration();
            var client = CreateClient();
            client.Configuration = config;
            ((ApiClient)client).Configuration.Should().BeSameAs(config);
        }

        protected ApiClient<CustomApiClientConfiguration> CreateClient(CustomApiClientConfiguration config = null)
        {
            return new Mock<ApiClient<CustomApiClientConfiguration>>(config).Object;
        }

    }

}
