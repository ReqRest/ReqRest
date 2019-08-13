namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Represents a builder for an object which provides an HTTP protocol version.
    /// </summary>
    public interface IHttpProtocolVersionBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP protocol version which the builder builds.
        /// </summary>
        Version Version { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpProtocolVersionBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpProtocolVersionBuilderExtensions
    {

        /// <summary>
        ///     Sets the HTTP protocol version which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="version">The HTTP protocol version.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetVersion<T>(this T builder, Version version) where T : IHttpProtocolVersionBuilder =>
            builder.Configure(builder => builder.Version = version);

    }

}
