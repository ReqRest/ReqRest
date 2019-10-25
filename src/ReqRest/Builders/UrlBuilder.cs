namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;

    // Before editing this class, consider editing the UriBuilderExtensions first.
    // This class is basically introduced for custom operators.
    // Most methods are applicable to every UriBuilder though, meaning that it makes sense
    // to implement them as extension methods.

    /// <summary>
    ///     An extension of the <see cref="UriBuilder"/> which overloads certain operators for
    ///     conveniently building URLs.
    ///     See remarks for details.
    /// </summary>
    /// <remarks>
    ///     Most features of this class are also available when using a normal <see cref="UriBuilder"/>
    ///     instance (via extension methods provided by the <see cref="UriBuilderExtensions"/> class).
    ///     This class is essentially introduced with the intention of overloading certain operators
    ///     so that URL building becomes more convenient and expressive. For example, the following
    ///     C# snippets have the same result:
    ///     
    ///     <code>
    ///     using ReqRest.Builders;
    ///     
    ///     UriBuilder uriBuilder = new UriBuilder();
    ///     UrlBuilder urlBuilder = new UrlBuilder();
    ///     
    ///     _ = uriBuilder.AppendPath("foo").AppendPath("bar");
    ///     _ = urlBuilder / "foo" / "bar";
    ///     
    ///     _ = uriBuilder.AppendQueryParameter("foo", "bar");
    ///     _ = urlBuilder &amp; ("foo", "bar");
    ///     </code>
    ///     
    ///     One difference between the <see cref="UriBuilder"/> and <see cref="UrlBuilder"/> is that
    ///     <see cref="UrlBuilder"/> implements the <see cref="IBuilder"/> interface. Therefore,
    ///     a variety of additional extension methods is available for this class.
    /// </remarks>
    public class UrlBuilder : UriBuilder, IBuilder
    {

        /// <summary>
        ///     Initializes a new <see cref="UrlBuilder"/> instance which starts building
        ///     on the specified <paramref name="uri"/> string.
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
        ///     the specified URI component values.
        /// </summary>
        /// <param name="scheme">An Internet access protocol.</param>
        /// <param name="host">A DNS-style domain name or IP address.</param>
        /// <param name="port">An IP port number for the service.</param>
        /// <param name="path">The path to the internet resource.</param>
        /// <param name="extraValue">A query string and/or fragment identifier.</param>
        public UrlBuilder(
            string? scheme = "http",
            string? host = "localhost",
            int? port = -1,
            string? path = null,
            string? extraValue = null)
            : base(scheme, host, port ?? -1, path, extraValue) { }

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
        ///     a query parameters (similar to <c>&amp;key=value</c>) and appends it at the
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
        ///     a query parameters (similar to <c>&amp;key=value</c>) and appends it at the
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

    }

}
