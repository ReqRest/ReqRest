namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http.Headers;

    /// <summary>
    ///     Defines the  static methods for an <see cref="IHttpContentHeadersBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpContentHeadersBuilderExtensions
    {

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
        public static T AddContentHeader<T>(this T builder, string name)
            where T : IHttpContentHeadersBuilder
        {
            return builder.AddHeader<T, HttpContentHeaders>(name);
        }

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
        public static T AddContentHeader<T>(this T builder, string name, string? value)
            where T : IHttpContentHeadersBuilder
        {
            return builder.AddHeader<T, HttpContentHeaders>(name, value);
        }

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
        public static T AddContentHeader<T>(this T builder, string name, IEnumerable<string?>? values)
            where T : IHttpContentHeadersBuilder
        {
            return builder.AddHeader<T, HttpContentHeaders>(name, values);
        }

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
        public static T RemoveContentHeader<T>(this T builder, params string?[]? names)
            where T : IHttpContentHeadersBuilder
        {
            return builder.RemoveHeader<T, HttpContentHeaders>(names);
        }

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
        public static T SetContentHeader<T>(this T builder, string name)
            where T : IHttpContentHeadersBuilder
        {
            return builder.SetHeader<T, HttpContentHeaders>(name);
        }

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
        public static T SetContentHeader<T>(this T builder, string name, string? value)
            where T : IHttpContentHeadersBuilder
        {
            return builder.SetHeader<T, HttpContentHeaders>(name, value);
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
        public static T SetContentHeader<T>(this T builder, string name, IEnumerable<string?>? values)
            where T : IHttpContentHeadersBuilder
        {
            return builder.SetHeader<T, HttpContentHeaders>(name, values);
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
        public static T ClearContentHeaders<T>(this T builder)
            where T : IHttpContentHeadersBuilder
        {
            return builder.ClearHeaders<T, HttpContentHeaders>();
        }

    }

}
