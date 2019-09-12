namespace ReqRest.Tests.Builders.UriBuilderExtensions
{
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetPasswordTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("")]
        [InlineData("Hello")]
        [InlineData("WORLD")]
        public void Sets_Password_To_Expected_Value(string expected)
        {
            Builder.SetPassword(expected);
            Builder.Password.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_Password_To_Empty_String_If_Null_Is_Used()
        {
            Builder.SetPassword(null);
            Builder.Password.Should().BeEmpty();
        }

    }

}
