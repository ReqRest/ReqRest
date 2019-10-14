namespace ReqRest.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.TestRecipes;

    // A note about this file's test class structure:
    //
    // The extensions for the IHttpHeadersBuilder<T> come in two forms:
    // - "non-generic", e.g. AddHeader<THeaders>(this IHttpHeadersBuilder<THeaders>, string name)
    // - generic, e.g. AddHeader<TBuilder, THeaders>(this TBuilder, string name)
    //
    // For the second form, a type parameter must always be specified when calling the method,
    // but it gives the advantage that one can tell the compiler exactly which header type should
    // be modified, if a class implements the builder interface multiple times.
    //
    // This file tests both the generic and non-generic methods.
    // The test methods themselves are defined in external Recipe classes, because they are shared.
    // 
    // To ensure that everything works as planned, we test the generic method version for all 4
    // known header types, i.e. request, response, content and default headers.
    // This ensures that everything has been written/configured properly and that the CLR/compiler
    // plays along with the method design.

    public class HttpHeadersBuilderExtensionsTests
    {

        public class NonGenericAddHeaderTests : AddHeaderExtensionRecipe<IHttpHeadersBuilder<HttpHeaders>, HttpHeaders>
        {

            protected override void AddHeader(IHttpHeadersBuilder<HttpHeaders> builder, string name) =>
                HttpHeadersBuilderExtensions.AddHeader(builder, name);

            protected override void AddHeader(IHttpHeadersBuilder<HttpHeaders> builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.AddHeader(builder, name, value);

            protected override void AddHeader(IHttpHeadersBuilder<HttpHeaders> builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.AddHeader(builder, name, values);

        }

        public abstract class GenericAddHeaderTests<THeaders> : AddHeaderExtensionRecipe<IHttpHeadersBuilder<THeaders>, THeaders>
            where THeaders : HttpHeaders
        {

            protected override void AddHeader(IHttpHeadersBuilder<THeaders> builder, string name) =>
                HttpHeadersBuilderExtensions.AddHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, name);

            protected override void AddHeader(IHttpHeadersBuilder<THeaders> builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.AddHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, name, value);

            protected override void AddHeader(IHttpHeadersBuilder<THeaders> builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.AddHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, name, values);

        }
        public class GenericAddHeaderTests_HttpHeaders : GenericAddHeaderTests<HttpHeaders> { }
        public class GenericAddHeaderTests_HttpRequestHeaders : GenericAddHeaderTests<HttpRequestHeaders> { }
        public class GenericAddHeaderTests_HttpResponseHeaders : GenericAddHeaderTests<HttpResponseHeaders> { }
        public class GenericAddHeaderTests_HttpContentHeaders : GenericAddHeaderTests<HttpContentHeaders> { }


        public class NonGenericClearHeadersTests : ClearHeadersExtensionRecipe<IHttpHeadersBuilder<HttpHeaders>, HttpHeaders>
        {

            protected override void ClearHeaders(IHttpHeadersBuilder<HttpHeaders> builder) =>
                HttpHeadersBuilderExtensions.ClearHeaders(builder);

        }

        public abstract class GenericClearHeadersTests<THeaders> : ClearHeadersExtensionRecipe<IHttpHeadersBuilder<THeaders>, THeaders>
            where THeaders : HttpHeaders
        {

            protected override void ClearHeaders(IHttpHeadersBuilder<THeaders> builder) =>
                HttpHeadersBuilderExtensions.ClearHeaders<IHttpHeadersBuilder<THeaders>, THeaders>(builder);

        }
        public class GenericClearHeaderTests_HttpHeaders : GenericClearHeadersTests<HttpHeaders> { }
        public class GenericClearHeaderTests_HttpRequestHeaders : GenericClearHeadersTests<HttpRequestHeaders> { }
        public class GenericClearHeaderTests_HttpResponseHeaders : GenericClearHeadersTests<HttpResponseHeaders> { }
        public class GenericClearHeaderTests_HttpContentHeaders : GenericClearHeadersTests<HttpContentHeaders> { }


        public class NonGenericRemoveHeaderTests : RemoveHeaderExtensionRecipe<IHttpHeadersBuilder<HttpHeaders>, HttpHeaders>
        {

            protected override void RemoveHeader(IHttpHeadersBuilder<HttpHeaders> builder, params string?[]? names) =>
                HttpHeadersBuilderExtensions.RemoveHeader(builder, names);

        }

        public abstract class GenericRemoveHeaderTests<THeaders> : RemoveHeaderExtensionRecipe<IHttpHeadersBuilder<THeaders>, THeaders>
            where THeaders : HttpHeaders
        {

            protected override void RemoveHeader(IHttpHeadersBuilder<THeaders> builder, params string?[]? names) =>
                HttpHeadersBuilderExtensions.RemoveHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, names);

        }
        public class GenericRemoveHeaderTests_HttpHeaders : GenericRemoveHeaderTests<HttpHeaders> { }
        public class GenericRemoveHeaderTests_HttpRequestHeaders : GenericRemoveHeaderTests<HttpRequestHeaders> { }
        public class GenericRemoveHeaderTests_HttpResponseHeaders : GenericRemoveHeaderTests<HttpResponseHeaders> { }
        public class GenericRemoveHeaderTests_HttpContentHeaders : GenericRemoveHeaderTests<HttpContentHeaders> { }


        public class NonGenericSetHeaderTests : SetHeaderExtensionRecipe<IHttpHeadersBuilder<HttpHeaders>, HttpHeaders>
        {

            protected override void SetHeader(IHttpHeadersBuilder<HttpHeaders> builder, string name) =>
                HttpHeadersBuilderExtensions.SetHeader(builder, name);

            protected override void SetHeader(IHttpHeadersBuilder<HttpHeaders> builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.SetHeader(builder, name, value);

            protected override void SetHeader(IHttpHeadersBuilder<HttpHeaders> builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.SetHeader(builder, name, values);

        }

        public abstract class GenericSetHeaderTests<THeaders> : SetHeaderExtensionRecipe<IHttpHeadersBuilder<THeaders>, THeaders>
            where THeaders : HttpHeaders
        {

            protected override void SetHeader(IHttpHeadersBuilder<THeaders> builder, string name) =>
                HttpHeadersBuilderExtensions.SetHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, name);
                                                      
            protected override void SetHeader(IHttpHeadersBuilder<THeaders> builder, string name, string? value) =>
                HttpHeadersBuilderExtensions.SetHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, name, value);
                                                      
            protected override void SetHeader(IHttpHeadersBuilder<THeaders> builder, string name, IEnumerable<string?>? values) =>
                HttpHeadersBuilderExtensions.SetHeader<IHttpHeadersBuilder<THeaders>, THeaders>(builder, name, values);

        }
        public class GenericSetHeaderTests_HttpHeaders : GenericSetHeaderTests<HttpHeaders> { }
        public class GenericSetHeaderTests_HttpRequestHeaders : GenericSetHeaderTests<HttpRequestHeaders> { }
        public class GenericSetHeaderTests_HttpResponseHeaders : GenericSetHeaderTests<HttpResponseHeaders> { }
        public class GenericSetHeaderTests_HttpContentHeaders : GenericSetHeaderTests<HttpContentHeaders> { }


        public class NonGenericConfigureHeadersTests : ConfigureHeadersExtensionRecipe<IHttpHeadersBuilder<HttpHeaders>, HttpHeaders>
        {

            protected override void ConfigureHeaders(IHttpHeadersBuilder<HttpHeaders> builder, Action<HttpHeaders> configure) =>
                HttpHeadersBuilderExtensions.ConfigureHeaders(builder, configure);

        }

        public abstract class GenericConfigureHeadersTests<THeaders> : ConfigureHeadersExtensionRecipe<IHttpHeadersBuilder<THeaders>, THeaders>
            where THeaders : HttpHeaders
        {

            protected override void ConfigureHeaders(IHttpHeadersBuilder<THeaders> builder, Action<THeaders> configure) =>
                HttpHeadersBuilderExtensions.ConfigureHeaders<IHttpHeadersBuilder<THeaders>, THeaders>(builder, configure);

        }
        public class GenericConfigureHeadersTests_HttpHeaders : GenericConfigureHeadersTests<HttpHeaders> { }
        public class GenericConfigureHeadersTests_HttpRequestHeaders : GenericConfigureHeadersTests<HttpRequestHeaders> { }
        public class GenericConfigureHeadersTests_HttpResponseHeaders : GenericConfigureHeadersTests<HttpResponseHeaders> { }
        public class GenericConfigureHeadersTests_HttpContentHeaders : GenericConfigureHeadersTests<HttpContentHeaders> { }

    }

}
