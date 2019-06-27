namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;
    using System.Net;

    /// <summary>
    ///     Represents a builder for an HTTP status code.
    /// </summary>
    public interface IHttpStatusCodeBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP status code of the which the builder builds.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpStatusCodeBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpStatusCodeBuilderExtensions
    {

        /// <summary>
        ///     Sets the HTTP status code which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetStatusCode<T>(this T builder, int statusCode) where T : IHttpStatusCodeBuilder =>
            builder.SetStatusCode((HttpStatusCode)statusCode);
        
        /// <summary>
        ///     Sets the HTTP status code which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetStatusCode<T>(this T builder, HttpStatusCode statusCode) where T : IHttpStatusCodeBuilder =>
            builder.Configure(_ =>builder.StatusCode = statusCode);

    }

}
