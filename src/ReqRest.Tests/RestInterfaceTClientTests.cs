namespace ReqRest.Tests
{
    using Moq;
    using ReqRest.Tests.Sdk.Models;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;

    public class RestInterfaceTClientTests
    {

        public class ClientTests : TestBase<RestInterface<RestClient<CustomRestClientConfiguration>>>
        {

            protected override RestInterface<RestClient<CustomRestClientConfiguration>> CreateService()
            {
                var client = new Mock<RestClient<CustomRestClientConfiguration>>(null).Object;
                return new Mock<RestInterface<RestClient<CustomRestClientConfiguration>>>(client, null).Object;
            }

            [Fact]
            public void Returns_Client_Of_Base_Class()
            {
                Assert.Same(Service.Client, ((RestInterface)Service).Client);
            }

        }

    }

}
