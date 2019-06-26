namespace ReqRest.Builders.Tests.UriBuilderExtensions
{
    using FluentAssertions;
    using Xunit;

    public class SetQueryTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("")]
        [InlineData("?")]
        [InlineData("?foo=bar")]
        public void Sets_Query_To_Expected_Value(string expected)
        {
            Builder.SetQuery(expected);
            Builder.Query.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_Query_To_Empty_String_If_Null_Is_Used()
        {
            Builder.SetQuery(null);
            Builder.Query.Should().BeEmpty();
        }

    }

}
