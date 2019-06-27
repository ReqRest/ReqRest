namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http.Headers;

    /// <summary>
    ///     Represents a builder for an object which provides <see cref="HttpHeaders"/>.
    /// </summary>
    public interface IHttpHeadersBuilder : IBuilder
    {

        /// <summary>
        ///     Gets the collection of HTTP headers which the builder builds.
        /// </summary>
        HttpHeaders Headers { get; }

    }

    /// <summary>
    ///     Defines the  static methods for an <see cref="IHttpHeadersBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpHeadersBuilderExtensions
    {

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
            builder.ConfigureHeaders(headers => HttpHeadersExtensions.AddWithUnknownValueCount(headers, name, values));

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
            return builder.Configure(_ =>configureHeaders(builder.Headers));
        }

    }

}
