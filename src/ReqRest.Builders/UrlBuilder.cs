namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;

    // Before editing this class, consider editing the UriBuilderExtensions first.
    // This class is basically introduced for custom operators.
    // Most methods are applicable to every UriBuilder though, meaning that it makes sense
    // to implement them as extension methods.

    /// <summary>
    ///     An extension of the <see cref="UriBuilder"/> which is specifically tailored for
    ///     convenience and expressive code during URL creation.
    ///     It additionally implements <see cref="IBuilder"/>, thus enabling the default set
    ///     of builder extension methods on this class.
    /// </summary>
    public class UrlBuilder : UriBuilder, IBuilder
    {

        // These values match the default .NET implementation.
        private const int NoPort = -1;
        private const string DefaultScheme = "http";
        private const string DefaultHost = "localhost";

        /// <summary>
        ///     Initializes a new <see cref="UrlBuilder"/> instance which starts building
        ///     on the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">A string representing a URL.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="uri"/>
        /// </exception>
        public UrlBuilder(string uri)
            : base(uri) { }

        /// <summary>
        ///     Initializes a new <see cref="UrlBuilder"/> instance which starts building
        ///     on the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">An <see cref="Uri"/> representing a URL.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="uri"/>
        /// </exception>
        public UrlBuilder(Uri uri)
            : base(uri) { }

        /// <summary>
        ///     Initializes a new <see cref="UrlBuilder"/> instance which starts building on
        ///     an URL built from the specified values.
        /// </summary>
        /// <param name="scheme">The URL's scheme.</param>
        /// <param name="host">The URL's host.</param>
        /// <param name="port">The URL's port.</param>
        /// <param name="path">The URL's path.</param>
        /// <param name="extraValue">The URL's extra value.</param>
        public UrlBuilder(
            string? scheme = DefaultScheme, 
            string? host = DefaultHost, 
            int? port = NoPort, 
            string? path = null,
            string? extraValue = null)
            : base(scheme, host, port ?? NoPort, path, extraValue) { }

        /// <summary>
        ///     Appends the specified <paramref name="pathSegment"/> to the builder's
        ///     <see cref="UriBuilder.Path"/> and returns the same builder instance.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="pathSegment">
        ///     The segment to be appended to the builder's path.
        ///     
        ///     If <see langword="null"/> or empty, a single slash is appended instead,
        ///     resulting in a double slash <c>//</c>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        public static UrlBuilder operator /(UrlBuilder builder, string? pathSegment) =>
            builder.AppendPath(pathSegment);

        /// <summary>
        ///     Formats the specified parameter consisting of a key and value into
        ///     a query parameters (similar to <c>&amp;key=value)</c> and appends it at the
        ///     end of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameter starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameter.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="queryParameter">
        ///     A query parameter consisting of a key and value which should 
        ///     be appended to the query.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        public static UrlBuilder operator &(UrlBuilder builder, (string? Key, string? Value) queryParameter) =>
            builder.AppendQueryParameter(queryParameter);

        /// <summary>
        ///     Formats the specified parameter consisting of a key and value into
        ///     a query parameters (similar to <c>&amp;key=value)</c> and appends it at the
        ///     end of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameter starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameter.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="queryParameter">
        ///     A query parameter consisting of a key and value which should 
        ///     be appended to the query.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        public static UrlBuilder operator &(UrlBuilder builder, KeyValuePair<string?, string?> queryParameter) =>
            builder.AppendQueryParameter(queryParameter);

        /// <summary>
        ///     Appends the specified <paramref name="queryParameter"/> at the end of the
        ///     <see cref="UriBuilder.Query"/>.
        ///     
        ///     If the query ends with or if the <paramref name="queryParameter"/> starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameter.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="queryParameter">
        ///     The query parameter to be appeneded to the query.
        ///     
        ///     This can be <see langword="null"/>. If so (or if empty), nothing gets appended.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        public static UrlBuilder operator &(UrlBuilder builder, string? queryParameter) =>
            builder.AppendQueryParameter(queryParameter);

    }

}
