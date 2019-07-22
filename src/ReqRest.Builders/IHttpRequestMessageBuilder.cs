namespace ReqRest.Builders
{
    using System;
    using System.Net.Http;

    /// <summary>
    ///     Represents a builder for a <see cref="System.Net.Http.HttpRequestMessage"/>.
    /// </summary>
    public interface IHttpRequestMessageBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the <see cref="System.Net.Http.HttpRequestMessage"/> which the builder builds.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        HttpRequestMessage HttpRequestMessage { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpRequestMessageBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpRequestMessageBuilderExtensions
    {

        /// <summary>
        ///     Executes the specified <paramref name="configureRequest"/> function to modify the
        ///     <see cref="HttpRequestMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configureRequest">
        ///     A function which receives the builder's 
        ///     <see cref="IHttpRequestMessageBuilder.HttpRequestMessage"/> object.
        ///     The function can then modify the message as desired.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configureRequest"/>
        /// </exception>
        public static T ConfigureRequest<T>(
            this T builder, Action<HttpRequestMessage> configureRequest) where T : IHttpRequestMessageBuilder
        {
            _ = configureRequest ?? throw new ArgumentNullException(nameof(configureRequest));
            return ConfigureRequest(builder, req =>
            {
                configureRequest(req);
                return req;
            });
        }

        /// <summary>
        ///     Executes the specified <paramref name="setRequest"/> function to change
        ///     the <see cref="HttpRequestMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="setRequest">
        ///     A function which must return an <see cref="HttpRequestMessage"/> which is then used
        ///     as the builder's new request message.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="setRequest"/>
        ///     * The result of <paramref name="setRequest"/> is <see langword="null"/>.
        /// </exception>
        public static T ConfigureRequest<T>(
            this T builder, Func<HttpRequestMessage> setRequest) where T : IHttpRequestMessageBuilder
        {
            _ = setRequest ?? throw new ArgumentNullException(nameof(setRequest));
            return ConfigureRequest(builder, _ => setRequest());
        }

        /// <summary>
        ///     Executes the specified <paramref name="setRequest"/> function to modify and/or change
        ///     the <see cref="HttpRequestMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="setRequest">
        ///     A function which receives the builder's 
        ///     <see cref="IHttpRequestMessageBuilder.HttpRequestMessage"/> object.
        ///     The function must return an <see cref="HttpRequestMessage"/> which is then used
        ///     as the builder's new request message.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="setRequest"/>
        ///     * The result of <paramref name="setRequest"/> is <see langword="null"/>.
        /// </exception>
        public static T ConfigureRequest<T>(
            this T builder, Func<HttpRequestMessage, HttpRequestMessage> setRequest) where T : IHttpRequestMessageBuilder
        {
            _ = setRequest ?? throw new ArgumentNullException(nameof(setRequest));
            return builder.Configure(_ =>builder.HttpRequestMessage = setRequest(builder.HttpRequestMessage));
        }

        /// <summary>
        ///     Sets the <see cref="HttpRequestMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="httpRequestMessage">
        ///     The new <see cref="HttpRequestMessage"/> to be used.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="httpRequestMessage"/>
        /// </exception>
        public static T SetRequest<T>(this T builder, HttpRequestMessage httpRequestMessage)
            where T : IHttpRequestMessageBuilder
        {
            _ = httpRequestMessage ?? throw new ArgumentNullException(nameof(httpRequestMessage));
            return builder.Configure(_ =>builder.HttpRequestMessage = httpRequestMessage);
        }

    }

}
