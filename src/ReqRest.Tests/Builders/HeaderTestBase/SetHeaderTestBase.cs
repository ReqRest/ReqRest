namespace ReqRest.Tests.Builders.HeaderTestBase
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using FluentAssertions;
    using Xunit;

    public abstract class SetHeaderTestBase<THeaders> : AddHeaderTestBase<THeaders> where THeaders : HttpHeaders
    {

        protected override sealed void AddHeader(string name) =>
            SetHeader(name);

        protected override sealed void AddHeader(string name, string value) =>
            SetHeader(name, value);

        protected override sealed void AddHeader(string name, IEnumerable<string> values) =>
            SetHeader(name, values);

        protected abstract void SetHeader(string name);

        protected abstract void SetHeader(string name, string value);

        protected abstract void SetHeader(string name, IEnumerable<string> values);

        [Fact]
        public void NameOnly_Sets_Header()
        {
            var name = "Test";
            Headers.Add(name, "Previous");
            SetHeader(name);

            Headers.Should().Contain(header => 
                header.Key == name && 
                header.Value.SequenceEqual(new string[] { "" })
            );
        }

        [Theory]
        [InlineData("Test", new string[] { "Value" })]
        [InlineData("Test", new string[] { })]
        [InlineData("Test", null)]
        public void MultipleValues_Sets_Header(string name, string[] values)
        {
            Headers.Add(name, "Previous");
            SetHeader(name, values);

            Headers.Should().Contain(header =>
                header.Key == name &&
                (values == null || values.Count() == 0)
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.SequenceEqual(values)
            );
        }

        [Theory]
        [InlineData("Test", "Value")]
        [InlineData("Test", "")]
        [InlineData("Test", null)]
        public void SingleValue_Sets_Header(string name, string value)
        {
            Headers.Add(name, "Previous");
            AddHeader(name, value);

            Headers.Should().Contain(header =>
                header.Key == name &&
                value == null
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.Any(v => v == value)
            );
        }

    }

}
