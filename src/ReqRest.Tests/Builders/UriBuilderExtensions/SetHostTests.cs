namespace ReqRest.Tests.Builders.UriBuilderExtensions
{
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetHostTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("")]
        [InlineData("localhost")]
        [InlineData("127.0.0.1")]
        [InlineData("foobar")]
        public void Sets_Host_To_Expected_Value(string expected)
        {
            Builder.SetHost(expected);
            Builder.Host.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_Host_To_Empty_String_If_Null_Is_Used()
        {
            Builder.SetHost(null);
            Builder.Host.Should().BeEmpty();
        }

    }

}
