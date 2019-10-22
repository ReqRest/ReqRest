namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http.Headers;

    public static partial class HttpHeadersBuilderExtensions
    {

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
            this T builder, string mediaType, string? charSet = null, IEnumerable<NameValueHeaderValue>? parameters = null)
            where T : IHttpHeadersBuilder<HttpContentHeaders>
        {
            _ = mediaType ?? throw new ArgumentNullException(nameof(mediaType));

            var header = new MediaTypeHeaderValue(mediaType)
            {
                CharSet = charSet
            };

            // It sucks that the parameters cannot be set directly. This only leaves enumeration.
            if (!(parameters is null))
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
            where T : IHttpHeadersBuilder<HttpContentHeaders>
        {
            return builder.ConfigureHeaders<T, HttpContentHeaders>(headers => headers.ContentType = contentType);
        }

    }

}
