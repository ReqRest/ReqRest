namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    ///     Represents a builder for an HTTP request which provides a set of configurable properties.
    /// </summary>
    public interface IHttpRequestPropertiesBuilder : IBuilder
    {

        /// <summary>
        ///     Gets the request's set of properties which are being built.
        /// </summary>
        IDictionary<string, object?> Properties { get; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpRequestPropertiesBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpRequestPropertiesBuilderExtensions
    {

        /// <summary>
        ///     Adds the specified property and value to the properties of the HTTP request
        ///     which is being built.
        ///     If a property with the specified <paramref name="key"/> already exists,
        ///     it is overwritten.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="key">
        ///     The key of the property.
        /// </param>
        /// <param name="value">
        ///     The value of the property.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="key"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetProperty<T>(this T builder, string key, object? value) where T : IHttpRequestPropertiesBuilder =>
            builder.ConfigureProperties(p =>
            {
                _ = key ?? throw new ArgumentNullException(nameof(key));
                p[key] = value;
            });

        /// <summary>
        ///     Adds the specified property and value to the properties of the HTTP request which
        ///     is being built.
        ///     If a property with the specified <paramref name="key"/> already exists, an exception
        ///     is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="key">
        ///     The key of the property.
        /// </param>
        /// <param name="value">
        ///     The value of the property.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     The properties already contain a property with the <paramref name="key"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="key"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AddProperty<T>(this T builder, string key, object? value) where T : IHttpRequestPropertiesBuilder =>
            builder.ConfigureProperties(p =>
            {
                _ = key ?? throw new ArgumentNullException(nameof(key));
                p.Add(key, value);
            });

        /// <summary>
        ///     Removes the properties with the specified names from the properties of the
        ///     HTTP request which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="names">
        ///     The names of the properties to be removed.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T RemoveProperty<T>(this T builder, params string?[]? names) where T : IHttpRequestPropertiesBuilder =>
            builder.ConfigureProperties(p =>
            {
                if (names is null) return;

                foreach (var name in names)
                {
                    if (name != null)
                    {
                        p.Remove(name);
                    }
                }
            });

        /// <summary>
        ///     Removes all properties from the properties of the HTTP request which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ClearProperties<T>(this T builder) where T : IHttpRequestPropertiesBuilder =>
            builder.ConfigureProperties(p => p.Clear());

        /// <summary>
        ///     Executes the specified <paramref name="configureProperties"/> function to modify the
        ///     properties of the HTTP request which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configureProperties">
        ///     A function which receives the request's properties.
        ///     The function can then modify the properties as desired.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configureProperties"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureProperties<T>(
            this T builder, Action<IDictionary<string, object?>> configureProperties) where T : IHttpRequestPropertiesBuilder
        {
            _ = configureProperties ?? throw new ArgumentNullException(nameof(configureProperties));
            return builder.Configure(() => configureProperties(builder.Properties));
        }

    }

}
