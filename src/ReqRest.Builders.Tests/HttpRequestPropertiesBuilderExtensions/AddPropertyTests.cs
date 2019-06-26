namespace ReqRest.Builders.Tests.HttpRequestPropertiesBuilderExtensions
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class AddPropertyTests: HttpRequestBuilderTestBase
    {

        [Theory]
        [InlineData("Test", "Value")]
        [InlineData("Test", null)]
        public void AddProperty_Adds_Property(string key, object value)
        {
            Builder.AddProperty(key, value);
            Builder.HttpRequestMessage.Properties.Should()
                .ContainKey(key)
                .WhichValue.Should().BeSameAs(value);
        }

        [Fact]
        public void AddProperty_Throws_ArgumentNullException_For_Key()
        {
            Action testCode = () => Builder.AddProperty(null, new object());
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddProperty_Throws_ArgumentException_If_Key_Already_Exists()
        {
            var key = "Key";
            Builder.AddProperty(key, new object());

            Action testCode = () => Builder.AddProperty(key, new object());
            testCode.Should().Throw<ArgumentException>();
        }

    }

}
