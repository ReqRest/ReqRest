namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    ///     Implements several builder interfaces which enable fluent building of
    ///     <see cref="System.Net.Http.HttpRequestMessage"/> objects.
    /// </summary>
    public class HttpRequestMessageBuilder : 
        IBuilder,
        IHttpRequestMessageBuilder,
        IHttpHeadersBuilder,
        IHttpRequestPropertiesBuilder,
        IHttpContentBuilder,
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

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HttpHeaders IHttpHeadersBuilder.Headers => HttpRequestMessage.Headers;

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IDictionary<string, object?> IHttpRequestPropertiesBuilder.Properties => HttpRequestMessage.Properties;

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HttpContent? IHttpContentBuilder.Content
        {
            get => HttpRequestMessage.Content;
            set => HttpRequestMessage.Content = value;
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Version IHttpProtocolVersionBuilder.Version
        {
            get => HttpRequestMessage.Version;
            set => HttpRequestMessage.Version = value;
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Uri? IRequestUriBuilder.RequestUri
        {
            get => HttpRequestMessage.RequestUri;
            set => HttpRequestMessage.RequestUri = value;
        }

        /// <inheritdoc/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HttpMethod IHttpMethodBuilder.Method
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
