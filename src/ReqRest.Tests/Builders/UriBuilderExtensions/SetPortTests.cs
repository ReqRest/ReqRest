namespace ReqRest.Tests.Builders.UriBuilderExtensions
{
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetPortTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(123)]
        [InlineData(65535)]
        public void Sets_Port_To_Expected_Value(int expected)
        {
            Builder.SetPort(expected);
            Builder.Port.Should().Be(expected);
        }

        [Fact]
        public void Sets_Port_To_Negative_One_If_Null_Is_Used()
        {
            Builder.SetPort(null);
            Builder.Port.Should().Be(-1);
        }

    }

}
