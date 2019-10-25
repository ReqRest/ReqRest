namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Defines the static methods for an <see cref="IRequestUriBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class RequestUriBuilderExtensions
    {

        /// <summary>
        ///     Modifies the current request URI by invoking the specified <paramref name="configure"/>
        ///     function which can then modify the <see cref="IRequestUriBuilder.RequestUri"/>
        ///     via an <see cref="UrlBuilder"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configure">
        ///     A function which receives a new <see cref="UrlBuilder"/> instance created from the
        ///     current <see cref="IRequestUriBuilder.RequestUri"/>.
        ///     The URI which has been built by the returned <see cref="UrlBuilder"/> is used as the
        ///     new <see cref="IRequestUriBuilder.RequestUri"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configure"/>
        /// </exception>
        /// <exception cref="UriFormatException">
        ///     The <see cref="Uri"/> which was built by the <paramref name="configure"/> method
        ///     had an invalid format.
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureRequestUri<T>(this T builder, Action<UrlBuilder> configure) where T : IRequestUriBuilder
        {
            _ = configure ?? throw new ArgumentNullException(nameof(configure));
            
            return builder.ConfigureRequestUri(urlBuilder =>
            {
                configure(urlBuilder);
                return urlBuilder;
            });
        }

        /// <summary>
        ///     Modifies the current request URI by invoking the specified <paramref name="configure"/>
        ///     function which can then modify the <see cref="IRequestUriBuilder.RequestUri"/>
        ///     via an <see cref="UrlBuilder"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configure">
        ///     A function which receives a new <see cref="UrlBuilder"/> instance created from the
        ///     current <see cref="IRequestUriBuilder.RequestUri"/>.
        ///     The URI which has been built by the returned <see cref="UrlBuilder"/> is used as the
        ///     new <see cref="IRequestUriBuilder.RequestUri"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configure"/>
        /// </exception>
        /// <exception cref="UriFormatException">
        ///     The <see cref="Uri"/> which was built by the <paramref name="configure"/> function
        ///     had an invalid format.
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureRequestUri<T>(this T builder, Func<UrlBuilder, UriBuilder?> configure) where T : IRequestUriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            var urlBuilder = builder.RequestUri is null
                ? new UrlBuilder()
                : new UrlBuilder(builder.RequestUri);
            var result = configure(urlBuilder);
            return builder.SetRequestUri(result?.Uri);
        }

        /// <summary>
        ///     Modifies the current request URI by invoking the specified <paramref name="configure"/>
        ///     function which can then modify the <see cref="IRequestUriBuilder.RequestUri"/>
        ///     via an <see cref="UrlBuilder"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configure">
        ///     A function which receives a new <see cref="UrlBuilder"/> instance created from the
        ///     current <see cref="IRequestUriBuilder.RequestUri"/>.
        ///     The returned <see cref="Uri"/> is used as the new <see cref="IRequestUriBuilder.RequestUri"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configure"/>
        /// </exception>
        /// <exception cref="UriFormatException">
        ///     The <see cref="Uri"/> which was built by the <paramref name="configure"/> function
        ///     had an invalid format.
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureRequestUri<T>(this T builder, Func<UrlBuilder, Uri?> configure) where T : IRequestUriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            var urlBuilder = builder.RequestUri is null
                ? new UrlBuilder()
                : new UrlBuilder(builder.RequestUri);
            var uri = configure(urlBuilder);
            return builder.SetRequestUri(uri);
        }

        /// <summary>
        ///     Sets the request URI which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="requestUri">
        ///     A string which represents the request URI to be used.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="uriKind">
        ///     The kind of the <see cref="Uri"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetRequestUri<T>(this T builder, string? requestUri, UriKind uriKind = UriKind.RelativeOrAbsolute) 
            where T : IRequestUriBuilder =>
                builder.SetRequestUri(requestUri is null ? null : new Uri(requestUri, uriKind));

        /// <summary>
        ///     Sets the request URI which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="requestUri">
        ///     The request URI used.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetRequestUri<T>(this T builder, Uri? requestUri) where T : IRequestUriBuilder =>
            builder.Configure(builder => builder.RequestUri = requestUri);

    }

}
