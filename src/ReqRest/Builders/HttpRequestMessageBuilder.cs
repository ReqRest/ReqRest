namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ReqRest.Resources;

    /// <summary>
    ///     Implements several builder interfaces which enable fluent building of
    ///     <see cref="System.Net.Http.HttpRequestMessage"/> objects.
    /// </summary>
    public class HttpRequestMessageBuilder :
        IBuilder,
        IHttpRequestMessageBuilder,
        IHttpHeadersBuilder<HttpHeaders>,
        IHttpHeadersBuilder<HttpRequestHeaders>,
        IHttpHeadersBuilder<HttpContentHeaders>,
        IHttpContentBuilder,
        IHttpContentHeadersBuilder,
        IHttpRequestPropertiesBuilder,
        IHttpProtocolVersionBuilder,
        IRequestUriBuilder,
        IHttpMethodBuilder
    {

        private HttpRequestMessage _httpRequestMessage;

        /// <inheritdoc/>
        public HttpRequestMessage HttpRequestMessage
        {
            get => _httpRequestMessage;
            set => _httpRequestMessage = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        ///     Gets the <see cref="HttpContentHeaders"/> of the underlying 
        ///     <see cref="HttpContent"/>, if one exists.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     The underlying <see cref="HttpContent"/> is <see langword="null"/>.
        /// </exception>
        HttpContentHeaders IHttpHeadersBuilder<HttpContentHeaders>.Headers
        {
            get
            {
                if (Content is null)
                {
                    throw new InvalidOperationException(ExceptionStrings.IHttpContentHeadersBuilder_HttpContent_Is_Null());
                }
                return Content.Headers;
            }
        }

        /// <summary>
        ///     Gets the value of the <see cref="Headers"/> property.
        ///     These are the headers that are configured by default when using the
        ///     non-generic extension methods provided by <see cref="HttpHeadersBuilderExtensions"/>.
        /// </summary>
        HttpHeaders IHttpHeadersBuilder<HttpHeaders>.Headers => Headers;

        /// <summary>
        ///     Gets the collection of HTTP request headers
        ///     of the <see cref="HttpRequestMessage"/> whose properties are being built.
        /// </summary>
        public HttpRequestHeaders Headers => HttpRequestMessage.Headers;

        /// <summary>
        ///     Gets the set of HTTP properties for the request
        ///     of the <see cref="HttpRequestMessage"/> whose properties are being built.
        /// </summary>
        public IDictionary<string, object?> Properties => HttpRequestMessage.Properties;

        /// <summary>
        ///     Gets or sets the HTTP content
        ///     of the <see cref="HttpRequestMessage"/> whose properties are being built.
        /// </summary>
        public HttpContent? Content
        {
            get => HttpRequestMessage.Content;
            set => HttpRequestMessage.Content = value;
        }

        /// <summary>
        ///     Gets or sets the HTTP message version
        ///     of the <see cref="HttpRequestMessage"/> whose properties are being built.
        /// </summary>
        public Version Version
        {
            get => HttpRequestMessage.Version;
            set => HttpRequestMessage.Version = value;
        }

        /// <summary>
        ///     Gets or sets the request <see cref="Uri"/>
        ///     of the <see cref="HttpRequestMessage"/> whose properties are being built.
        /// </summary>
        public Uri? RequestUri
        {
            get => HttpRequestMessage.RequestUri;
            set => HttpRequestMessage.RequestUri = value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="HttpMethod"/>
        ///     of the <see cref="HttpRequestMessage"/> whose properties are being built.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public HttpMethod Method
        {
            get => HttpRequestMessage.Method;
            set => HttpRequestMessage.Method = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpRequestMessageBuilder"/> class
        ///     which starts building on the specified <paramref name="httpRequestMessage"/>.
        /// </summary>
        /// <param name="httpRequestMessage">
        ///     The request from which the builder starts building.
        ///     If <see langword="null"/>, a new instance is created instead.
        /// </param>
        public HttpRequestMessageBuilder(HttpRequestMessage? httpRequestMessage = null)
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            _httpRequestMessage = httpRequestMessage ?? new HttpRequestMessage();
#pragma warning restore CA2000 // Dispose objects before losing scope
        }

        /// <summary>
        ///     Returns a string representing the values of the underlying
        ///     <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <returns>
        ///     A string representing the values of the underlying <see cref="HttpRequestMessage"/>.
        /// </returns>
        public override string ToString() =>
            HttpRequestMessage.ToString();

    }

}
