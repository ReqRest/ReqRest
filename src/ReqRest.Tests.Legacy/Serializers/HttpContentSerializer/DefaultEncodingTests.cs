namespace ReqRest.Tests.Serializers.HttpContentSerializer
{
    using System.Text;
    using FluentAssertions;
    using ReqRest.Tests.Shared;
    using Xunit;

    public class DefaultEncodingTests
    {

        [Fact]
        public void Uses_UTF8_As_Default_Encoding()
        {
            new MockedHttpContentSerializer().DefaultEncoding.Should().BeSameAs(Encoding.UTF8);
        }

    }

}
