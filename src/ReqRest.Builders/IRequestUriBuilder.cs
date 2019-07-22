namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Represents a builder for an object which provides an <see cref="Uri"/> for making
    ///     an HTTP request.
    /// </summary>
    public interface IRequestUriBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the request <see cref="Uri"/> which the builder builds.
        ///     This can be <see langword="null"/>.
        /// </summary>
        Uri? RequestUri { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IRequestUriBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class RequestUriBuilderExtensions
    {

        /// <summary>
        ///     Executes the specified <paramref name="configure"/> function to modify the
        ///     <see cref="IRequestUriBuilder.RequestUri"/> with a new <see cref="UrlBuilder"/>
        ///     instance.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configure">
        ///     A function which receives a new <see cref="UrlBuilder"/> instance created on
        ///     the builder's current <see cref="IRequestUriBuilder.RequestUri"/>.
        ///     The function can then use the builder to modify the request URI.
        /// </param>
        /// <returns></returns>
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
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            var urlBuilder = builder.RequestUri is null
                ? new UrlBuilder()
                : new UrlBuilder(builder.RequestUri);

            configure(urlBuilder);
            return builder.SetRequestUri(urlBuilder.Uri);
        }

        /// <summary>
        ///     Sets the request <see cref="Uri"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="requestUri">
        ///     A string which represents the request <see cref="Uri"/> to be used.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetRequestUri<T>(this T builder, string? requestUri) where T : IRequestUriBuilder =>
            builder.SetRequestUri(requestUri is null ? null : new Uri(requestUri));

        /// <summary>
        ///     Sets the request <see cref="Uri"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="requestUri">
        ///     The request <see cref="Uri"/> used.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetRequestUri<T>(this T builder, Uri? requestUri) where T : IRequestUriBuilder =>
            builder.Configure(_ => builder.RequestUri = requestUri);

    }

}
