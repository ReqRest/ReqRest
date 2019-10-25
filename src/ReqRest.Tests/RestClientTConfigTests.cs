namespace ReqRest.Tests
{
    using System;
    using Moq;
    using ReqRest.Tests.Sdk.Models;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;

    public class RestClientTConfigTests
    {

        public class ConfigurationTests : TestBase<RestClient<CustomRestClientConfiguration>>
        {

            [Fact]
            public void Throws_InvalidCastException_If_Types_Dont_Match()
            {
                var config = new RestClientConfiguration();
                ((RestClient)Service).Configuration = config;
                Assert.Throws<InvalidCastException>(() => _ = Service.Configuration);
            }

            [Fact]
            public void Returns_Configuration_Of_Base_Class()
            {
                var config = new CustomRestClientConfiguration();
                ((RestClient)Service).Configuration = config;
                Assert.Same(config, Service.Configuration);
            }

            [Fact]
            public void Sets_Configuration_Of_Base_Class()
            {
                var config = new CustomRestClientConfiguration();
                Service.Configuration = config;
                Assert.Same(config, ((RestClient)Service).Configuration);
            }

            protected override RestClient<CustomRestClientConfiguration> CreateService()
            {
                return CreateService(null);
            }

            protected RestClient<CustomRestClientConfiguration> CreateService(CustomRestClientConfiguration? config)
            {
                return new Mock<RestClient<CustomRestClientConfiguration>>(config).Object;
            }

        }

    }

}
