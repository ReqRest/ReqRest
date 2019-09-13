namespace ReqRest.Tests.Builders.UriBuilderExtensions
{
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetPathTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("/")]
        [InlineData("//")]
        [InlineData("///")]
        [InlineData("a/b")]
        [InlineData("foo")]
        public void Sets_Path_To_Expected_Value(string expected)
        {
            Builder.SetPath(expected);
            Builder.Path.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_Path_To_Single_Slash_If_Null_Is_Used()
        {
            Builder.SetPath(null);
            Builder.Path.Should().Be("/");
        }

    }

}
