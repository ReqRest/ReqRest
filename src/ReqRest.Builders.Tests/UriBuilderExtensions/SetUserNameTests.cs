namespace ReqRest.Builders.Tests.UriBuilderExtensions
{
    using FluentAssertions;
    using Xunit;

    public class SetUserNameTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("")]
        [InlineData("user")]
        [InlineData("NAME")]
        public void Sets_UserName_To_Expected_Value(string expected)
        {
            Builder.SetUserName(expected);
            Builder.UserName.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_UserName_To_Empty_String_If_Null_Is_Used()
        {
            Builder.SetUserName(null);
            Builder.UserName.Should().BeEmpty();
        }

    }

}
