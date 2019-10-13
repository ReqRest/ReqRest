namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http.Headers;
    using ReqRest.Internal;

    /// <summary>
    ///     Defines the  static methods for an <see cref="IHttpHeadersBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpHeadersBuilderExtensions
    {

        #region IHttpHeadersBuilder

        /// <summary>
        ///     Adds the specified header without any value to the <see cref="HttpHeaders"/>
        ///     which are being built.
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
        public static T AddHeader<T>(this T builder, string name) where T : IHttpHeadersBuilder =>
            builder.AddHeader(name, value: null);

        /// <summary>
        ///     Adds the specified header and its value to the <see cref="HttpHeaders"/>
        ///     which are being built.
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
        public static T AddHeader<T>(this T builder, string name, string? value) where T : IHttpHeadersBuilder =>
            builder.ConfigureHeaders(headers => headers.Add(
                name ?? throw new ArgumentNullException(nameof(name)), 
                value
            ));

        /// <summary>
        ///     Adds the specified header and its values to the <see cref="HttpHeaders"/>
        ///     which are being built.
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
        public static T AddHeader<T>(
            this T builder, string name, IEnumerable<string?>? values) where T : IHttpHeadersBuilder =>
                builder.ConfigureHeaders(headers => headers.AddWithUnknownValueCount(name, values));

        /// <summary>
        ///     Removes the headers with the specified names from the <see cref="HttpHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="names">The names of the headers to be removed.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T RemoveHeader<T>(this T builder, params string?[]? names) where T : IHttpHeadersBuilder =>
            builder.ConfigureHeaders(headers => headers.Remove(names));

        /// <summary>
        ///     Sets the specified header without any value by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
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
        public static T SetHeader<T>(this T builder, string name) where T : IHttpHeadersBuilder =>
            builder.SetHeader(name, value: null);

        /// <summary>
        ///     Sets the specified header and its value by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
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
        public static T SetHeader<T>(this T builder, string name, string? value) where T : IHttpHeadersBuilder
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            return builder
                .RemoveHeader(name)
                .AddHeader(name, value);
        }

        /// <summary>
        ///     Sets the specified header and its values by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
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
        public static T SetHeader<T>(
            this T builder, string name, IEnumerable<string?>? values) where T : IHttpHeadersBuilder
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            return builder
                .RemoveHeader(name)
                .AddHeader(name, values);
        }

        /// <summary>
        ///     Removes all headers from the <see cref="HttpHeaders"/> which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ClearHeaders<T>(this T builder) where T : IHttpHeadersBuilder =>
            builder.ConfigureHeaders(h => h.Clear());

        /// <summary>
        ///     Executes the specified <paramref name="configureHeaders"/> function to modify the
        ///     <see cref="HttpHeaders"/> which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configureHeaders">
        ///     A function which receives the <see cref="HttpHeaders"/> object.
        ///     The function can then modify the headers as desired.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configureHeaders"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureHeaders<T>(
            this T builder, Action<HttpHeaders> configureHeaders) where T : IHttpHeadersBuilder
        {
            _ = configureHeaders ?? throw new ArgumentNullException(nameof(configureHeaders));
            return builder.Configure(builder => configureHeaders(builder.Headers));
        }

        #endregion

        #region IHttpHeadersBuilder<HttpContentHeaders>

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
            IEnumerable<NameValueHeaderValue>? parameters = null) where T : IHttpHeadersBuilder<HttpContentHeaders>
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
        public static T SetContentType<T>(this T builder, MediaTypeHeaderValue? contentType) 
            where T : IHttpHeadersBuilder<HttpContentHeaders> =>
                builder.ConfigureContentHeaders(headers => headers.ContentType = contentType);

        /// <summary>
        ///     Adds the specified header without any value to the <see cref="HttpContentHeaders"/>
        ///     which are being built.
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
        public static T AddContentHeader<T>(this T builder, string name) where T : IHttpHeadersBuilder<HttpContentHeaders> =>
            builder.AddContentHeader(name, value: null);

        /// <summary>
        ///     Adds the specified header and its value to the <see cref="HttpContentHeaders"/>
        ///     which are being built.
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
        public static T AddContentHeader<T>(this T builder, string name, string? value) where T : IHttpHeadersBuilder<HttpContentHeaders> =>
            builder.ConfigureContentHeaders(headers => headers.Add(
                name ?? throw new ArgumentNullException(nameof(name)),
                value
            ));

        /// <summary>
        ///     Adds the specified header and its values to the <see cref="HttpContentHeaders"/>
        ///     which are being built.
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
        public static T AddContentHeader<T>(this T builder, string name, IEnumerable<string?>? values) where T : IHttpHeadersBuilder<HttpContentHeaders> =>
            builder.ConfigureContentHeaders(headers => headers.AddWithUnknownValueCount(name, values));

        /// <summary>
        ///     Removes the headers with the specified names from the <see cref="HttpContentHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="names">The names of the headers to be removed.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T RemoveContentHeader<T>(this T builder, params string?[]? names) where T : IHttpHeadersBuilder<HttpContentHeaders> =>
            builder.ConfigureContentHeaders(headers => headers.Remove(names));

        /// <summary>
        ///     Sets the specified header without any value by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
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
        public static T SetContentHeader<T>(this T builder, string name) where T : IHttpHeadersBuilder<HttpContentHeaders> =>
            builder.SetHeader(name, value: null);

        /// <summary>
        ///     Sets the specified header and its value by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
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
        public static T SetContentHeader<T>(this T builder, string name, string? value) where T : IHttpHeadersBuilder<HttpContentHeaders>
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            return builder
                .RemoveHeader(name)
                .AddHeader(name, value);
        }

        /// <summary>
        ///     Sets the specified header and its values by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
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
        public static T SetContentHeader<T>(
            this T builder, string name, IEnumerable<string?>? values) where T : IHttpHeadersBuilder<HttpContentHeaders>
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            return builder
                .RemoveHeader(name)
                .AddHeader(name, values);
        }

        /// <summary>
        ///     Removes all headers from the <see cref="HttpContentHeaders"/> which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ClearContentHeaders<T>(this T builder) where T : IHttpHeadersBuilder<HttpContentHeaders> =>
            builder.ConfigureContentHeaders(headers => headers.Clear());

        /// <summary>
        ///     Executes the specified <paramref name="configureHeaders"/> function to modify the
        ///     <see cref="HttpContentHeaders"/> which are being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configureHeaders">
        ///     A function which receives the <see cref="HttpContentHeaders"/> object.
        ///     The function can then modify the headers as desired.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configureHeaders"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureContentHeaders<T>(
            this T builder, Action<HttpContentHeaders> configureHeaders) where T : IHttpHeadersBuilder<HttpContentHeaders>
        {
            _ = configureHeaders ?? throw new ArgumentNullException(nameof(configureHeaders));
            return builder.Configure(builder => configureHeaders(builder.Headers));
        }

        #endregion

    }

}
