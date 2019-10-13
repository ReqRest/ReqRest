namespace ReqRest.Tests.Builders.HeaderTestBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using FluentAssertions;
    using System.Net.Http.Headers;

    public abstract class AddHeaderTestBase<THeaders> : HttpHeadersTestBase<THeaders> where THeaders : HttpHeaders
    {

        protected abstract void AddHeader(string name);

        protected abstract void AddHeader(string name, string value);

        protected abstract void AddHeader(string name, IEnumerable<string> values);

        [Fact]
        public void NameOnly_Adds_Header()
        {
            var name = "Test";
            AddHeader(name);
            Headers.Should().Contain(header => header.Key == name);
        }

        [Fact]
        public void NameOnly_Throws_ArgumentNullException_For_Name()
        {
            Action testCode = () => AddHeader(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("Test", new string[] { "Value" })]
        [InlineData("Test", new string[] { })]
        [InlineData("Test", null)]
        public void MultipleValues_Adds_Header(string name, string[] values)
        {
            AddHeader(name, values);
            Headers.Should().Contain(header =>
                header.Key == name &&
                (values == null || values.Count() == 0)
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.SequenceEqual(values)
            );
        }

        [Fact]
        public void MultipleValues_Throws_ArgumentNullException_For_Name()
        {
            Action testCode = () => AddHeader(null, new string[0]);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("Test", "Value")]
        [InlineData("Test", "")]
        [InlineData("Test", null)]
        public void SingleValue_Adds_Header(string name, string value)
        {
            AddHeader(name, value);
            Headers.Should().Contain(header =>
                header.Key == name &&
                value == null
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.Any(v => v == value)
            );
        }

        [Fact]
        public void SingleValue_Throws_ArgumentNullException_For_Name()
        {
            Action testCode = () => AddHeader(null, "");
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
