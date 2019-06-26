namespace ReqRest
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    ///     Implements several builder interfaces which enable fluent building of
    ///     <see cref="System.Net.Http.HttpResponseMessage"/> objects.
    /// </summary>
    public class HttpResponseMessageBuilder :
        IHttpResponseMessageBuilder,
        IHttpHeadersBuilder,
        IHttpContentBuilder,
        IHttpProtocolVersionBuilder,
        IHttpResponseReasonPhraseBuilder,
        IHttpStatusCodeBuilder
    {

        private HttpResponseMessage _httpResponseMessage;

        /// <inheritdoc/>
        public HttpResponseMessage HttpResponseMessage
        {
            get => _httpResponseMessage;
            set => _httpResponseMessage = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HttpHeaders IHttpHeadersBuilder.Headers => HttpResponseMessage.Headers;

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HttpContent? IHttpContentBuilder.Content
        {
            get => HttpResponseMessage.Content;
            set => HttpResponseMessage.Content = value;
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Version IHttpProtocolVersionBuilder.Version
        {
            get => HttpResponseMessage.Version;
            set => HttpResponseMessage.Version = value;
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string? IHttpResponseReasonPhraseBuilder.ReasonPhrase
        {
            get => HttpResponseMessage.ReasonPhrase;
            set => HttpResponseMessage.ReasonPhrase = value;
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HttpStatusCode IHttpStatusCodeBuilder.StatusCode
        {
            get => HttpResponseMessage.StatusCode;
            set => HttpResponseMessage.StatusCode = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpResponseMessageBuilder"/> class
        ///     which starts building on the specified <paramref name="httpResponseMessage"/>.
        /// </summary>
        /// <param name="httpResponseMessage">
        ///     The response from which the builder starts building.
        ///     If <see langword="null"/>, a new instance is created instead.
        /// </param>
        public HttpResponseMessageBuilder(HttpResponseMessage? httpResponseMessage = null)
        {
            _httpResponseMessage = httpResponseMessage ?? new HttpResponseMessage();
        }

    }

}
