namespace ReqRest.Builders.Tests.UriBuilderExtensions
{
    using FluentAssertions;
    using Xunit;

    public class SetSchemeTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("")]
        [InlineData("http")]
        [InlineData("FOO")]
        public void Sets_Scheme_To_Expected_Value(string expected)
        {
            Builder.SetScheme(expected);
            Builder.Scheme.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_Scheme_To_Empty_String_If_Null_Is_Used()
        {
            Builder.SetScheme(null);
            Builder.Scheme.Should().BeEmpty();
        }

    }

}
