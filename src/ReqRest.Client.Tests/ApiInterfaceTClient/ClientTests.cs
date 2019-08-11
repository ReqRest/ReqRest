namespace ReqRest.Client.Tests.ApiInterfaceTClient
{
    using FluentAssertions;
    using Moq;
    using ReqRest.Client;
    using Xunit;

    public class ClientTests
    {
        
        [Fact]
        public void Returns_Client_Of_Base_Class()
        {
            var client = new Mock<ApiClient<CustomApiClientConfiguration>>(null).Object;
            var @interface = CreateInterface(client);
            ((ApiInterface)@interface).Client.Should().BeSameAs(@interface.Client);
        }

        protected ApiInterface<ApiClient<CustomApiClientConfiguration>> CreateInterface(ApiClient client)
        {
            return new Mock<ApiInterface<ApiClient<CustomApiClientConfiguration>>>(client, null).Object;
        }
    }

}
