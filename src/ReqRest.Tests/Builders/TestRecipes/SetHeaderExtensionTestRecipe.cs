namespace ReqRest.Tests.Builders.TestRecipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public abstract class SetHeaderExtensionTestRecipe<TBuilder> : HttpHeadersBuilderTestBase<TBuilder>
        where TBuilder : IHttpHeadersBuilder
    {

        protected abstract void SetHeader(TBuilder builder, string name);
        protected abstract void SetHeader(TBuilder builder, string name, string? value);
        protected abstract void SetHeader(TBuilder builder, string name, IEnumerable<string?>? values);

        [Fact]
        public void Name_Sets_Header()
        {
            var name = "Test";
            Builder.Headers.Add(name, "Previous");
            SetHeader(Builder, name);

            Assert.Contains(Builder.Headers, header =>
                header.Key == name &&
                header.Value.SequenceEqual(new string[] { "" })
            );
        }

        [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
        public void Name_Throws_ArgumentNullException(TBuilder builder, string name = "Test")
        {
            Assert.Throws<ArgumentNullException>(() => SetHeader(builder, name));
        }

        [Theory]
        [InlineData("Test", "Value")]
        [InlineData("Test", "")]
        [InlineData("Test", null)]
        public void NameValue_Sets_Header(string name, string? value)
        {
            Builder.Headers.Add(name, "Previous");
            SetHeader(Builder, name, value);

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
            Assert.Throws<ArgumentNullException>(() => SetHeader(builder, name, value));
        }

        [Theory]
        [InlineData("Test", new string?[] { "Value" })]
        [InlineData("Test", new string?[] { null })]
        [InlineData("Test", new string?[] { })]
        [InlineData("Test", null)]
        public void MultipleValues_Sets_Header(string name, string?[]? values)
        {
            Builder.Headers.Add(name, "Previous");
            SetHeader(Builder, name, values);

            Assert.Contains(Builder.Headers, header =>
                header.Key == name &&
                (values == null || !values.Where(v => v != null).Any())
                    ? header.Value.SequenceEqual(new string[] { "" })
                    : header.Value.SequenceEqual(values)
            );
        }

        [Theory, ArgumentNullExceptionData(NotNull, NotNull, Null)]
        public void MultipleValues_Throws_ArgumentNullException(TBuilder builder, string name = "Test", IEnumerable<string?>? values = null)
        {
            Assert.Throws<ArgumentNullException>(() => SetHeader(builder, name, values));
        }

    }

}
