namespace ReqRest.Builders.Tests.HttpRequestPropertiesBuilderExtensions
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class SetPropertiesTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [InlineData("Test", "Value")]
        [InlineData("Test", null)]
        public void SetProperty_Adds_Property(string key, object value)
        {
            Builder.SetProperty(key, value);
            Builder.HttpRequestMessage.Properties.Should()
                .ContainKey(key)
                .WhichValue.Should().BeSameAs(value);
        }

        [Fact]
        public void SetProperty_Throws_ArgumentNullException_For_Key()
        {
            Action testCode = () => Builder.SetProperty(null, new object());
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetProperty_Overwrites_If_Key_Already_Exists()
        {
            var key = "Key";
            Builder.SetProperty(key, new object());
            Builder.SetProperty(key, 123);
            Builder.HttpRequestMessage.Properties[key].Should().Be(123);
        }

    }

}
