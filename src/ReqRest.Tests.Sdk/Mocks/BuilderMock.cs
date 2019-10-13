namespace ReqRest.Tests.Sdk.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ReqRest.Builders;

    /// <summary>
    ///     A class mocking the behavior of every single builder interface defined by ReqRest.
    ///     In comparison to builders like the <see cref="HttpRequestMessageBuilder"/>, this class
    ///     doesn't forward properties to the underlying HTTP messages which allows extension
    ///     method tests to focus on the actual behavior.
    /// </summary>
    public sealed class BuilderMock :
        IBuilder,
        IHttpRequestMessageBuilder,
        IHttpResponseMessageBuilder,
        IHttpContentBuilder,
        IHttpHeadersBuilder,
        IHttpHeadersBuilder<HttpContentHeaders>,
        IHttpHeadersBuilder<HttpRequestHeaders>,
        IHttpHeadersBuilder<HttpResponseHeaders>,
        IHttpMethodBuilder,
        IHttpProtocolVersionBuilder,
        IHttpRequestPropertiesBuilder,
        IHttpResponseReasonPhraseBuilder,
        IHttpStatusCodeBuilder,
        IRequestUriBuilder
    {

        /// <summary>
        ///     Gets or sets the message for the <see cref="IHttpRequestMessageBuilder"/>.
        /// </summary>
        public HttpRequestMessage HttpRequestMessage { get; set; } = new HttpRequestMessage();

        /// <summary>
        ///     Gets or sets the message for the <see cref="IHttpResponseMessageBuilder"/>.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; } = new HttpResponseMessage();

        /// <summary>
        ///     Gets or sets the content for the <see cref="IHttpContentBuilder"/>.
        /// </summary>
        public HttpContent? Content { get; set; }

        /// <summary>
        ///     Gets the headers for the <see cref="IHttpHeadersBuilder"/>.
        /// </summary>
        public HttpHeaders Headers { get; } = new HttpRequestMessage().Headers;

        HttpContentHeaders IHttpHeadersBuilder<HttpContentHeaders>.Headers => ContentHeaders;

        /// <summary>
        ///     Gets the headers for the <see cref="IHttpHeadersBuilder{HttpContentHeaders}"/>.
        /// </summary>
        public HttpContentHeaders ContentHeaders { get; } = new ByteArrayContent(Array.Empty<byte>()).Headers;

        HttpRequestHeaders IHttpHeadersBuilder<HttpRequestHeaders>.Headers => RequestHeaders;

        /// <summary>
        ///     Gets the headers for the <see cref="IHttpHeadersBuilder{HttpRequestHeaders}"/>.
        /// </summary>
        public HttpRequestHeaders RequestHeaders { get; } = new HttpRequestMessage().Headers;

        HttpResponseHeaders IHttpHeadersBuilder<HttpResponseHeaders>.Headers => ResponseHeaders;

        /// <summary>
        ///     Gets the headers for the <see cref="IHttpHeadersBuilder{HttpResponseHeaders}"/>.
        /// </summary>
        public HttpResponseHeaders ResponseHeaders { get; } = new HttpResponseMessage().Headers;

        /// <summary>
        ///     Gets or sets the HTTP method for the <see cref="IHttpMethodBuilder"/>.
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.Get;

        /// <summary>
        ///     Gets or sets the version for the <see cref="IHttpProtocolVersionBuilder"/>.
        /// </summary>
        public Version Version { get; set; } = new Version();

        /// <summary>
        ///     Gets the properties for the <see cref="IHttpRequestPropertiesBuilder"/>.
        /// </summary>
        public IDictionary<string, object?> Properties { get; } = new Dictionary<string, object?>();

        /// <summary>
        ///     Gets or sets the reason phrase for the <see cref="IHttpResponseReasonPhraseBuilder"/>.
        /// </summary>
        public string? ReasonPhrase { get; set; }

        /// <summary>
        ///     Gets or sets the HTTP status code for the <see cref="IHttpStatusCodeBuilder"/>.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        ///     Gets or sets the URI for the <see cref="IRequestUriBuilder"/>.
        /// </summary>
        public Uri? RequestUri { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BuilderMock"/> class with default values.
        /// </summary>
        public BuilderMock() { }

    }

}
