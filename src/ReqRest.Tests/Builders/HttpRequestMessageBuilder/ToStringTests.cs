namespace ReqRest.Tests.Builders.HttpRequestMessageBuilder
{
    using FluentAssertions;
    using Xunit;
    using ReqRest.Builders;

    public class ToStringTests
    {

        [Fact]
        public void ToString_Returns_ToString_Result_Of_HttpRequestMessage()
        {
            var builder = new HttpRequestMessageBuilder();
            var expected = builder.HttpRequestMessage.ToString();
            var actual = builder.ToString();
            actual.Should().BeEquivalentTo(expected);
        }
        
    }

}
