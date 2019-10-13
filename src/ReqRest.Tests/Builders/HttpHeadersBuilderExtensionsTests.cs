namespace ReqRest.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.TestRecipes;

    public class HttpHeadersBuilderExtensionsTests
    {

        public class AddHeaderTests : AddHeaderExtensionRecipe<IHttpHeadersBuilder>
        {

            protected override void AddHeader(IHttpHeadersBuilder builder, string name) =>
                HttpHeadersBuilderExtensions.AddHeader(builder, name);

            protected override void AddHeader(IHttpHeadersBuilder builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.AddHeader(builder, name, value);

            protected override void AddHeader(IHttpHeadersBuilder builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.AddHeader(builder, name, values);

        }

        public class ClearHeadersTests : ClearHeadersExtensionRecipe<IHttpHeadersBuilder>
        {

            protected override void ClearHeaders(IHttpHeadersBuilder builder) =>
                HttpHeadersBuilderExtensions.ClearHeaders(builder);

        }
        
        public class ConfigureHeadersTests : ConfigureHeadersExtensionRecipe<IHttpHeadersBuilder, HttpHeaders>
        {

            protected override void ConfigureHeaders(IHttpHeadersBuilder builder, Action<HttpHeaders> configure) =>
                HttpHeadersBuilderExtensions.ConfigureHeaders(builder, configure);

        }

        public class RemoveHeaderTests : RemoveHeaderExtensionRecipe<IHttpHeadersBuilder>
        {

            protected override void RemoveHeader(IHttpHeadersBuilder builder, params string?[]? names) =>
                HttpHeadersBuilderExtensions.RemoveHeader(builder, names);

        }

        public class SetHeaderTests : SetHeaderExtensionTestRecipe<IHttpHeadersBuilder>
        {

            protected override void SetHeader(IHttpHeadersBuilder builder, string name) =>
                HttpHeadersBuilderExtensions.SetHeader(builder, name);

            protected override void SetHeader(IHttpHeadersBuilder builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.SetHeader(builder, name, value);

            protected override void SetHeader(IHttpHeadersBuilder builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.SetHeader(builder, name, values);

        }

        public class AddContentHeaderTests : AddHeaderExtensionRecipe<IHttpHeadersBuilder<HttpContentHeaders>>
        {

            protected override void AddHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, string name) =>
                HttpHeadersBuilderExtensions.AddContentHeader(builder, name);

            protected override void AddHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.AddContentHeader(builder, name, value);

            protected override void AddHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.AddContentHeader(builder, name, values);

        }

        public class ClearContentHeadersTests : ClearHeadersExtensionRecipe<IHttpHeadersBuilder<HttpContentHeaders>>
        {

            protected override void ClearHeaders(IHttpHeadersBuilder<HttpContentHeaders> builder) =>
                HttpHeadersBuilderExtensions.ClearContentHeaders(builder);

        }

        public class ConfigureContentHeadersTests : ConfigureHeadersExtensionRecipe<IHttpHeadersBuilder<HttpContentHeaders>, HttpContentHeaders>
        {

            protected override void ConfigureHeaders(IHttpHeadersBuilder<HttpContentHeaders> builder, Action<HttpContentHeaders> configure) =>
                HttpHeadersBuilderExtensions.ConfigureContentHeaders(builder, configure);

        }

        public class RemoveContentHeaderTests : RemoveHeaderExtensionRecipe<IHttpHeadersBuilder<HttpContentHeaders>>
        {

            protected override void RemoveHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, params string?[]? names) =>
                HttpHeadersBuilderExtensions.RemoveContentHeader(builder, names);

        }

        public class SetContentHeaderTests : SetHeaderExtensionTestRecipe<IHttpHeadersBuilder<HttpContentHeaders>>
        {

            protected override void SetHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, string name) =>
                HttpHeadersBuilderExtensions.SetContentHeader(builder, name);

            protected override void SetHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.SetContentHeader(builder, name, value);

            protected override void SetHeader(IHttpHeadersBuilder<HttpContentHeaders> builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.SetContentHeader(builder, name, values);

        }

    }

}
