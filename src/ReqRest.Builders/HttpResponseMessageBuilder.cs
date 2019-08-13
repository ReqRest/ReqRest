namespace ReqRest.Builders
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    ///     Implements several builder interfaces which enable fluent building of
    ///     <see cref="System.Net.Http.HttpResponseMessage"/> objects.
    /// </summary>
    public class HttpResponseMessageBuilder :
        IHttpResponseMessageBuilder,
        IHttpHeadersBuilder<HttpResponseHeaders>,
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
        HttpHeaders IHttpHeadersBuilder.Headers => Headers;

        /// <summary>
        ///     Gets the collection of HTTP response headers
        ///     of the <see cref="HttpResponseMessage"/> whose properties are being built.
        /// </summary>
        public HttpResponseHeaders Headers => HttpResponseMessage.Headers;

        /// <summary>
        ///     Gets or sets the HTTP content 
        ///     of the <see cref="HttpResponseMessage"/> whose properties are being built.
        /// </summary>
        public HttpContent? Content
        {
            get => HttpResponseMessage.Content;
            set => HttpResponseMessage.Content = value;
        }

        /// <summary>
        ///     Gets or sets the HTTP message version
        ///     of the <see cref="HttpResponseMessage"/> whose properties are being built.
        /// </summary>
        public Version Version
        {
            get => HttpResponseMessage.Version;
            set => HttpResponseMessage.Version = value;
        }

        /// <summary>
        ///     Gets or sets the reason phrase
        ///     of the <see cref="HttpResponseMessage"/> whose properties are being built.
        /// </summary>
        public string? ReasonPhrase
        {
            get => HttpResponseMessage.ReasonPhrase;
            set => HttpResponseMessage.ReasonPhrase = value;
        }

        /// <summary>
        ///     Gets or sets the _HTTP status code
        ///     of the <see cref="HttpResponseMessage"/> whose properties are being built.
        /// </summary>
        public HttpStatusCode StatusCode
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

        /// <summary>
        ///     Returns a string representing the values of the underlying
        ///     <see cref="HttpResponseMessage"/>.
        /// </summary>
        /// <returns>
        ///     A string representing the values of the underlying <see cref="HttpResponseMessage"/>.
        /// </returns>
        public override string ToString() =>
            HttpResponseMessage.ToString();

    }

}
