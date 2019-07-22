namespace ReqRest.Serializers.Tests.HttpContentSerializer
{
    using System.Text;
    using FluentAssertions;
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
