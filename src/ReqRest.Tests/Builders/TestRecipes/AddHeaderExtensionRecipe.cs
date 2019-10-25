namespace ReqRest.Tests.Builders.TestRecipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public abstract class AddHeaderExtensionRecipe<TBuilder, THeaders> : HttpHeadersBuilderTestBase<TBuilder, THeaders>
        where TBuilder : IHttpHeadersBuilder<THeaders>
        where THeaders : HttpHeaders
    {

        protected abstract void AddHeader(TBuilder builder, string name);
        protected abstract void AddHeader(TBuilder builder, string name, string? value);
        protected abstract void AddHeader(TBuilder builder, string name, IEnumerable<string?>? values);

        [Fact]
        public void Name_Adds_Empty_Header()
        {
            var headerName = "Test";
            AddHeader(Builder, headerName);
            Assert.Contains(Builder.Headers, header =>
                   header.Key == headerName
                && header.Value.Count() == 1
                && header.Value.First().Length == 0
            );
        }

        [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
        public void Name_Throws_ArgumentNullException(TBuilder builder, string name = "Test")
        {
            Assert.Throws<ArgumentNullException>(() => AddHeader(builder, name));
        }

        [Theory]
        [InlineData("Test", "Value")]
        [InlineData("Test", "")]
        [InlineData("Test", null)]
        public void NameValue_Adds_Header(string name, string value)
        {
            AddHeader(Builder, name, value);
            Assert.Contains(Builder.Headers, header =>
                header.Key == name &&
                value == null
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.Any(v => v == value)
            );
        }

        [Theory, ArgumentNullExceptionData(NotNull, NotNull, Null)]
        public void NameValue_Throws_ArgumentNullException(TBuilder builder, string name = "Test", string? value = null)
        {
            Assert.Throws<ArgumentNullException>(() => AddHeader(builder, name, value));
        }

        [Theory]
        [InlineData("Test", new string?[] { "Value" })]
        [InlineData("Test", new string?[] { null })]
        [InlineData("Test", new string?[] { })]
        [InlineData("Test", null)]
        public void MultipleValues_Adds_Header(string name, string?[]? values)
        {
            AddHeader(Builder, name, values);
            Assert.Contains(Builder.Headers, header =>
                header.Key == name &&
                (values == null || !values.Where(v => !(v is null)).Any())
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.SequenceEqual(values)
            );
        }

        [Theory, ArgumentNullExceptionData(NotNull, NotNull, Null)]
        public void MultipleValues_Throws_ArgumentNullException(TBuilder builder, string name = "Test", IEnumerable<string?>? values = null)
        {
            Assert.Throws<ArgumentNullException>(() => AddHeader(builder, name, values));
        }

    }

}
