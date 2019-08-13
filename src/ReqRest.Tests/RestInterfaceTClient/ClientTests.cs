namespace ReqRest.Tests.RestInterfaceTClient
{
    using FluentAssertions;
    using Moq;
    using ReqRest;
    using Xunit;

    public class ClientTests
    {
        
        [Fact]
        public void Returns_Client_Of_Base_Class()
        {
            var client = new Mock<RestClient<CustomRestClientConfiguration>>(null).Object;
            var @interface = CreateInterface(client);
            ((RestInterface)@interface).Client.Should().BeSameAs(@interface.Client);
        }

        protected RestInterface<RestClient<CustomRestClientConfiguration>> CreateInterface(RestClient client)
        {
            return new Mock<RestInterface<RestClient<CustomRestClientConfiguration>>>(client, null).Object;
        }
    }

}
