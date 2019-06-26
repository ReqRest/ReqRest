namespace ReqRest
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
            builder.Configure(() => builder.RequestUri = requestUri);

    }

}
