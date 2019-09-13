namespace ReqRest.Tests.Builders.HttpRequestPropertiesBuilderExtensions
{
    using System;
    using ReqRest.Builders;
    using FluentAssertions;
    using Xunit;

    public class ConfigurePropertiesTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void ConfigureProperties_Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.ConfigureProperties(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConfigureProperties_Invokes_Action()
        {
            var wasCalled = false;
            Builder.ConfigureProperties(_ => wasCalled = true);
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void ConfigureProperties_Passes_Requests_Properties()
        {
            Builder.ConfigureProperties(Properties =>
            {
                Properties.Should().BeSameAs(Builder.HttpRequestMessage.Properties);
            });
        }

    }

}
