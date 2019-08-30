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
        ///     
        ///     If the existing path ends with a single slash, or if the <paramref name="pathSegment"/>
        ///     starts with a single slash, the slashes are stripped, so that the resulting path
        ///     only has a single slash between the two concatenated parts.
        ///     
        ///     If the existing path starts with multiple slashes, or if the <paramref name="pathSegment"/> 
        ///     starts with multiple slashes, they are kept and appended to each other.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="pathSegment">
        ///     The segment to be appended to the builder's path.
        ///     
        ///     This can be <see langword="null"/>.
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
            builder.AppendQueryParameter(queryParameter.Key, queryParameter.Value);

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
            builder.AppendQueryParameter(queryParameter.Key, queryParameter.Value);

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
        ///     The query parameter to be appended to the query.
        ///     
        ///     This can be <see langword="null"/>. If so (or if empty), nothing gets appended.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        public static UrlBuilder operator &(UrlBuilder builder, string? queryParameter) =>
            builder.AppendQueryParameter(queryParameter);

        /// <summary>
        ///     Implicitly converts the specified <paramref name="builder"/> to an <see cref="Uri"/>
        ///     by returning the value of the <see cref="UriBuilder.Uri"/> property.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        ///     The <see cref="Uri"/> built by the builder or <see langword="null"/> if
        ///     <paramref name="builder"/> is <see langword="null"/>.
        /// </returns>
        /// <exception cref="UriFormatException">
        ///     The URI constructed by the <see cref="UriBuilder"/> properties is invalid.
        /// </exception>
        // TODO: [NotNullIfNotNull(nameof(builder))]
        public static implicit operator Uri?(UrlBuilder builder) =>
            builder?.Uri;

    }

}
