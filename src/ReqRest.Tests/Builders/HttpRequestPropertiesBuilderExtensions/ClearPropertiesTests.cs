namespace ReqRest.Tests.Builders.HttpRequestPropertiesBuilderExtensions
{
    using ReqRest.Builders;
    using FluentAssertions;
    using Xunit;

    public class ClearPropertiesTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void ClearProperties_Clears_Properties()
        {
            Builder
                .AddProperty("Test", "Property")
                .ClearProperties();
            Builder.HttpRequestMessage.Properties.Should().BeEmpty();
        }

    }

}
