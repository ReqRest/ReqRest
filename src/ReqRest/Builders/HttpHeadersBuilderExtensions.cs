namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http.Headers;
    using ReqRest.Internal;

    /// <summary>
    ///     Defines the  static methods for an <see cref="IHttpHeadersBuilder{T}"/> provided
    ///     by the library.
    /// </summary>
    public static partial class HttpHeadersBuilderExtensions
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
        public static T AddHeader<T>(this T builder, string name)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.AddHeader<T, HttpHeaders>(name, value: null);
        }

        /// <summary>
        ///     Adds the specified header without any value to the <see cref="HttpHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>s
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
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
        public static TBuilder AddHeader<TBuilder, THeaders>(this TBuilder builder, string name)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            return builder.AddHeader<TBuilder, THeaders>(name, value: null);
        }

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
        public static T AddHeader<T>(this T builder, string name, string? value) 
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.AddHeader<T, HttpHeaders>(name, value);
        }

        /// <summary>
        ///     Adds the specified header and its value to the <see cref="HttpHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
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
        public static TBuilder AddHeader<TBuilder, THeaders>(this TBuilder builder, string name, string? value)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            return builder.ConfigureHeaders<TBuilder, THeaders>(headers => headers.Add(name, value));
        }

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
        public static T AddHeader<T>(this T builder, string name, IEnumerable<string?>? values)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.AddHeader<T, HttpHeaders>(name, values);
        }

        /// <summary>
        ///     Adds the specified header and its values to the <see cref="HttpHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
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
        public static TBuilder AddHeader<TBuilder, THeaders>(this TBuilder builder, string name, IEnumerable<string?>? values)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            return builder.ConfigureHeaders<TBuilder, THeaders>(
                headers => headers.AddWithUnknownValueCount(name, values)
            );
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
        public static T SetHeader<T>(this T builder, string name)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.SetHeader<T, HttpHeaders>(name);
        }

        /// <summary>
        ///     Sets the specified header without any value by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
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
        public static TBuilder SetHeader<TBuilder, THeaders>(this TBuilder builder, string name)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            return builder.SetHeader<TBuilder, THeaders>(name, value: null);
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
        public static T SetHeader<T>(this T builder, string name, string? value)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.SetHeader<T, HttpHeaders>(name, value);
        }


        /// <summary>
        ///     Sets the specified header and its value by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
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
        public static TBuilder SetHeader<TBuilder, THeaders>(this TBuilder builder, string name, string? value)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            return builder
                .RemoveHeader<TBuilder, THeaders>(name)
                .AddHeader<TBuilder, THeaders>(name, value);
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
        public static T SetHeader<T>(this T builder, string name, IEnumerable<string?>? values)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.SetHeader<T, HttpHeaders>(name, values);
        }

        /// <summary>
        ///     Sets the specified header and its values by first removing any previous header
        ///     value with the specified <paramref name="name"/> and then adding the new one.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
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
        public static TBuilder SetHeader<TBuilder, THeaders>(this TBuilder builder, string name, IEnumerable<string?>? values)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            return builder
                .RemoveHeader<TBuilder, THeaders>(name)
                .AddHeader<TBuilder, THeaders>(name, values);
        }

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
        public static T RemoveHeader<T>(this T builder, params string?[]? names)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.RemoveHeader<T, HttpHeaders>(names);
        }

        /// <summary>
        ///     Removes the headers with the specified names from the <see cref="HttpHeaders"/>
        ///     which are being built.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="names">The names of the headers to be removed.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static TBuilder RemoveHeader<TBuilder, THeaders>(this TBuilder builder, params string?[]? names)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            return builder.ConfigureHeaders<TBuilder, THeaders>(headers => headers.Remove(names));
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
        public static T ClearHeaders<T>(this T builder)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.ClearHeaders<T, HttpHeaders>();
        }

        /// <summary>
        ///     Removes all headers from the <see cref="HttpHeaders"/> which are being built.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/> which are being built.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static TBuilder ClearHeaders<TBuilder, THeaders>(this TBuilder builder)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            return builder.ConfigureHeaders<TBuilder, THeaders>(h => h.Clear());
        }

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
        public static T ConfigureHeaders<T>(this T builder, Action<HttpHeaders> configureHeaders)
            where T : IHttpHeadersBuilder<HttpHeaders>
        {
            return builder.ConfigureHeaders<T, HttpHeaders>(configureHeaders);
        }

        /// <summary>
        ///     Executes the specified <paramref name="configureHeaders"/> function to modify the
        ///     <see cref="HttpHeaders"/> which are being built.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="THeaders">
        ///     The type of the headers to be configured.
        ///     This must be <see cref="HttpHeaders"/> type of one of the <see cref="IHttpHeadersBuilder{T}"/>
        ///     interfaces that the builder implements.
        /// </typeparam>
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
        public static TBuilder ConfigureHeaders<TBuilder, THeaders>(this TBuilder builder, Action<THeaders> configureHeaders)
            where TBuilder : IHttpHeadersBuilder<THeaders>
            where THeaders : HttpHeaders
        {
            _ = configureHeaders ?? throw new ArgumentNullException(nameof(configureHeaders));
            return builder.Configure(builder => configureHeaders(builder.Headers));
        }

    }

}
