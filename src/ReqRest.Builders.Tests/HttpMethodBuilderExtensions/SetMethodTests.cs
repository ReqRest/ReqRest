namespace ReqRest.Builders.Tests.HttpMethodBuilderExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Xunit;

    public class SetMethodTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void SetMethod_String_Sets_Method()
        {
            var name = "TEST";
            Builder.SetMethod(name);
            Builder.HttpRequestMessage.Method.Method.Should().Be(name);
        }

        [Fact]
        public void SetMethod_String_Throws_ArgumentNullException_For_Method()
        {
            Action testCode = () => Builder.SetMethod((string)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetMethod_HttpMethod_Sets_Method()
        {
            var expected = new HttpMethod("TEST");
            Builder.SetMethod(expected);
            Builder.HttpRequestMessage.Method.Should().BeSameAs(expected);
        }

        [Fact]
        public void SetMethod_HttpMethod_Throws_ArgumentNullException_For_Method()
        {
            Action testCode = () => Builder.SetMethod((HttpMethod)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
