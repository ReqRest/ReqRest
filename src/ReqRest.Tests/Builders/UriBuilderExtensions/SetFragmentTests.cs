namespace ReqRest.Tests.Builders.UriBuilderExtensions
{
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetFragmentTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [InlineData("")]
        [InlineData("#")]
        [InlineData("#foo")]
        public void Sets_Fragment_To_Expected_Value(string expected)
        {
            Builder.SetFragment(expected);
            Builder.Fragment.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sets_Fragment_To_Empty_String_If_Null_Is_Used()
        {
            Builder.SetFragment(null);
            Builder.Fragment.Should().BeEmpty();
        }

    }

}
