namespace ReqRest.Builders.Tests.HttpResponseMessageBuilder
{
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class ConstructorTests 
    {

        [Fact]
        public void Builder_Creates_New_Response()
        {
            var builder = new HttpResponseMessageBuilder();
            builder.HttpResponseMessage.Should().NotBeNull();
        }

        [Fact]
        public void Builder_Uses_Specified_Response()
        {
            var req = new HttpResponseMessage();
            var builder = new HttpResponseMessageBuilder(req);
            builder.HttpResponseMessage.Should().BeSameAs(req);
        }

    }

}
