﻿namespace ReqRest.Builders
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ReqRest.Resources;

    /// <summary>
    ///     Implements several builder interfaces which enable fluent building of
    ///     <see cref="System.Net.Http.HttpResponseMessage"/> objects.
    /// </summary>
    public class HttpResponseMessageBuilder :
        IBuilder,
        IHttpResponseMessageBuilder,
        IHttpHeadersBuilder<HttpHeaders>,
        IHttpHeadersBuilder<HttpResponseHeaders>,
        IHttpHeadersBuilder<HttpContentHeaders>,
        IHttpContentBuilder,
        IHttpContentHeadersBuilder,
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

        /// <summary>
        ///     Gets the content headers of the underlying <see cref="HttpContent"/>, if one exists.
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
        ///     Gets or sets the HTTP status code
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
