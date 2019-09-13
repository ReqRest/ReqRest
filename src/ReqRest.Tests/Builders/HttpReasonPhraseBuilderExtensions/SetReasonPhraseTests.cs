namespace ReqRest.Tests.Builders.HttpReasonPhraseBuilderExtensions
{
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetReasonPhraseTests : HttpResponseBuilderTestBase
    {

        [Theory]
        [InlineData("Reason phrase")]
        [InlineData("")]
        public void Sets_ReasonPhrase(string reasonPhrase)
        {
            Builder.SetReasonPhrase(reasonPhrase);
            ((IHttpResponseReasonPhraseBuilder)Builder).ReasonPhrase.Should().Be(reasonPhrase);
        }

        [Fact]
        public void Can_Set_ReasonPhrase_To_Null()
        {
            Builder.SetReasonPhrase(null); // Should not throw.
        }

    }

}
