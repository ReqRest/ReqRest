namespace ReqRest.Tests.Builders.HttpResponseMessageBuilder
{
    using FluentAssertions;
    using Xunit;
    using ReqRest.Builders;

    public class ToStringTests
    {

        [Fact]
        public void ToString_Returns_ToString_Result_Of_HttpResponseMessage()
        {
            var builder = new HttpResponseMessageBuilder();
            var expected = builder.HttpResponseMessage.ToString();
            var actual = builder.ToString();
            actual.Should().BeEquivalentTo(expected);
        }
        
    }

}
