namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using ReqRest.Resources;

    /// <summary>
    ///     Represents a builder for an object which provides an <see cref="HttpContent"/>.
    /// </summary>
    public interface IHttpContentBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP content which the builder builds.
        ///     This can be <see langword="null"/>.
        /// </summary>
        HttpContent? Content { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpContentBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpContentBuilderExtensions
    {

        #region SetContent

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="StringContent"/> instance which is created
        ///     from the specified <paramref name="content"/> and <paramref name="encoding"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The new HTTP content as a string.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, the default value of the <see cref="StringContent"/>
        ///     class is used.
        /// </param>
        /// <param name="mediaType">
        ///     The media type used by the content.
        ///     If <see langword="null"/>, the default value of the <see cref="StringContent"/>
        ///     class is used.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContent<T>(
            this T builder,
            string content,
            Encoding? encoding = null,
            string? mediaType = null) where T : IHttpContentBuilder
        {
#pragma warning disable CA2000
            _ = content ?? throw new ArgumentNullException(nameof(content));
            return builder.SetContent(new StringContent(content, encoding, mediaType));
#pragma warning restore CA2000
        }

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="ByteArrayContent"/> instance which is
        ///     created from the specified <paramref name="content"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The new HTTP content as a byte array.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContent<T>(this T builder, byte[] content) where T : IHttpContentBuilder =>
            builder.SetContent(content, 0, content?.Length ?? 0);

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="ByteArrayContent"/> instance which is
        ///     created from the specified <paramref name="content"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The new HTTP content as a byte array.</param>
        /// <param name="offset">A zero-based offset in <paramref name="content"/> from which sending starts.</param>
        /// <param name="count">The number of bytes to be sent, starting from <paramref name="offset"/>.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContent<T>(this T builder, byte[] content, int offset, int count) where T : IHttpContentBuilder
        {
#pragma warning disable CA2000 
            _ = content ?? throw new ArgumentNullException(nameof(content));
            return builder.SetContent(new ByteArrayContent(content, offset, count));
#pragma warning restore CA2000 
        }

        /// <summary>
        ///     Sets the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">
        ///     The new HTTP content.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContent<T>(this T builder, HttpContent content) where T : IHttpContentBuilder =>
            builder.Configure(_ =>builder.Content = content);

        #endregion

        #region SetContentType

        /// <summary>
        ///     Sets the <c>Content-Type</c> header of the <see cref="HttpContentHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="mediaType">
        ///     The media-type of the <c>Content-Type</c> header.
        /// </param>
        /// <param name="charSet">
        ///     The character set of the <c>Content-Type</c> header.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="parameters">
        ///     The media-type header value parameters of the <c>Content-Type</c> header.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContentType<T>(
            this T builder,
            string mediaType,
            string? charSet = null,
            IEnumerable<NameValueHeaderValue>? parameters = null) where T : IHttpContentBuilder
        {
            _ = mediaType ?? throw new ArgumentNullException(nameof(mediaType));

            var header = new MediaTypeHeaderValue(mediaType)
            {
                CharSet = charSet
            };

            // It sucks that the parameters cannot be set directly. This only leaves enumeration.
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    header.Parameters.Add(param);
                }
            }

            return builder.SetContentType(header);
        }

        /// <summary>
        ///     Sets the <c>Content-Type</c> header of the <see cref="HttpContentHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="contentType">The value for the <c>Content-Type</c> header.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContentType<T>(this T builder, MediaTypeHeaderValue? contentType) where T : IHttpContentBuilder =>
            builder.ConfigureContentHeaders(headers => headers.ContentType = contentType);

        #endregion

        #region Headers

        /// <summary>
        ///     Adds the specified header without any value to the <see cref="HttpContent.Headers"/>
        ///     of the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">
        ///     The name of the header.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="name"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AddContentHeader<T>(this T builder, string name) where T : IHttpContentBuilder =>
            builder.AddContentHeader(name, value: null);

        /// <summary>
        ///     Adds the specified header and its value to the <see cref="HttpContent.Headers"/>
        ///     of the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">
        ///     The name of the header.
        /// </param>
        /// <param name="value">
        ///     The content/value of the header.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="name"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AddContentHeader<T>(this T builder, string name, string? value) where T : IHttpContentBuilder =>
            builder.ConfigureContentHeaders(headers => headers.Add(
                name ?? throw new ArgumentNullException(nameof(name)),
                value
            ));

        /// <summary>
        ///     Adds the specified header and its values to the <see cref="HttpContent.Headers"/>
        ///     of the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">
        ///     The name of the header.
        /// </param>
        /// <param name="values">
        ///     The content/values of the header.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="name"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AddContentHeader<T>(this T builder, string name, IEnumerable<string?>? values) where T : IHttpContentBuilder =>
            builder.ConfigureContentHeaders(headers => headers.AddWithUnknownValueCount(name, values));

        /// <summary>
        ///     Removes the headers with the specified names from the <see cref="HttpContent.Headers"/>
        ///     of the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="names">The names of the headers to be removed.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T RemoveContentHeader<T>(this T builder, params string?[]? names) where T : IHttpContentBuilder =>
            builder.ConfigureContentHeaders(headers => headers.Remove(names));

        /// <summary>
        ///     Removes all headers from the <see cref="HttpContent.Headers"/> of the HTTP content
        ///     which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ClearContentHeaders<T>(this T builder) where T : IHttpContentBuilder =>
            builder.ConfigureContentHeaders(headers => headers.Clear());

        /// <summary>
        ///     Executes the specified <paramref name="configureHeaders"/> function to modify the
        ///     <see cref="HttpContent.Headers"/> of the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configureHeaders">
        ///     A function which receives the <see cref="HttpContent.Headers"/> object.
        ///     The function can then modify the headers as desired.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configureHeaders"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureContentHeaders<T>(
            this T builder, Action<HttpContentHeaders> configureHeaders) where T : IHttpContentBuilder
        {
            _ = configureHeaders ?? throw new ArgumentNullException(nameof(configureHeaders));
            return builder.Configure(_ =>
            {
                // There may not always be an HttpContent. There is nothing we can/should do. Fail here.
                if (builder.Content is null)
                {
                    throw new InvalidOperationException(ExceptionStrings.HttpContentBuilderExtensions_NoHttpContentHeaders);
                }

                configureHeaders(builder.Content.Headers);
            });
        }

        #endregion

    }

}
