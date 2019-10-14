namespace ReqRest.Tests.Builders
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.TestRecipes;

    public class HttpContentHeadersBuilderExtensionsTests
    {

        public class AddContentHeaderTests : AddHeaderExtensionRecipe<IHttpContentHeadersBuilder, HttpContentHeaders>
        {

            protected override void AddHeader(IHttpContentHeadersBuilder builder, string name) =>
                HttpContentHeadersBuilderExtensions.AddContentHeader(builder, name);

            protected override void AddHeader(IHttpContentHeadersBuilder builder, string name, string? value) =>
                HttpContentHeadersBuilderExtensions.AddContentHeader(builder, name, value);

            protected override void AddHeader(IHttpContentHeadersBuilder builder, string name, IEnumerable<string?>? values) =>
                HttpContentHeadersBuilderExtensions.AddContentHeader(builder, name, values);

        }

        public class ClearContentHeadersTests : ClearHeadersExtensionRecipe<IHttpContentHeadersBuilder, HttpContentHeaders>
        {

            protected override void ClearHeaders(IHttpContentHeadersBuilder builder) =>
                HttpContentHeadersBuilderExtensions.ClearContentHeaders(builder);

        }

        public class RemoveContentHeaderTests : RemoveHeaderExtensionRecipe<IHttpContentHeadersBuilder, HttpContentHeaders>
        {

            protected override void RemoveHeader(IHttpContentHeadersBuilder builder, params string?[]? names) =>
                HttpContentHeadersBuilderExtensions.RemoveContentHeader(builder, names);

        }

        public class SetContentHeaderTests : SetHeaderExtensionRecipe<IHttpContentHeadersBuilder, HttpContentHeaders>
        {

            protected override void SetHeader(IHttpContentHeadersBuilder builder, string name) =>
                HttpContentHeadersBuilderExtensions.SetContentHeader(builder, name);

            protected override void SetHeader(IHttpContentHeadersBuilder builder, string name, string? value) =>
                HttpContentHeadersBuilderExtensions.SetContentHeader(builder, name, value);

            protected override void SetHeader(IHttpContentHeadersBuilder builder, string name, IEnumerable<string?>? values) =>
                HttpContentHeadersBuilderExtensions.SetContentHeader(builder, name, values);

        }

    }

}
