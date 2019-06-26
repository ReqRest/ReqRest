namespace ReqRest
{
    using System;
    using System.Net.Http;
    using System.Diagnostics;

    /// <summary>
    ///     Represents a builder for a <see cref="System.Net.Http.HttpResponseMessage"/>.
    /// </summary>
    public interface IHttpResponseMessageBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the <see cref="System.Net.Http.HttpResponseMessage"/> which the builder builds.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        HttpResponseMessage HttpResponseMessage { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpResponseMessageBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpResponseMessageBuilderExtensions
    {

        /// <summary>
        ///     Executes the specified <paramref name="configureResponse"/> function to modify the
        ///     <see cref="HttpResponseMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configureResponse">
        ///     A function which receives the builder's 
        ///     <see cref="IHttpResponseMessageBuilder.HttpResponseMessage"/> object.
        ///     The function can then modify the response as desired.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configureResponse"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureResponse<T>(
            this T builder, Action<HttpResponseMessage> configureResponse) where T : IHttpResponseMessageBuilder
        {
            _ = configureResponse ?? throw new ArgumentNullException(nameof(configureResponse));
            return ConfigureResponse(builder, req =>
            {
                configureResponse(req);
                return req;
            });
        }

        /// <summary>
        ///     Executes the specified <paramref name="setResponse"/> function to change
        ///     the <see cref="HttpResponseMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="setResponse">
        ///     A function which must return an
        ///     <see cref="IHttpResponseMessageBuilder.HttpResponseMessage"/> which is then used
        ///     as the builder's new response.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="setResponse"/>
        ///     * The result of <paramref name="setResponse"/> is <see langword="null"/>.
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureResponse<T>(
            this T builder, Func<HttpResponseMessage> setResponse) where T : IHttpResponseMessageBuilder
        {
            _ = setResponse ?? throw new ArgumentNullException(nameof(setResponse));
            return ConfigureResponse(builder, _ => setResponse());
        }

        /// <summary>
        ///     Executes the specified <paramref name="setResponse"/> function to modify and/or change
        ///     the <see cref="HttpResponseMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="setResponse">
        ///     A function which receives the builder's
        ///     <see cref="IHttpResponseMessageBuilder.HttpResponseMessage"/> object.
        ///     The function must return an <see cref="HttpResponseMessage"/> which is then used
        ///     as the builder's new response.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="setResponse"/>
        ///     * The result of <paramref name="setResponse"/> is <see langword="null"/>.
        /// </exception>
        [DebuggerStepThrough]
        public static T ConfigureResponse<T>(
            this T builder, Func<HttpResponseMessage, HttpResponseMessage> setResponse) where T : IHttpResponseMessageBuilder
        {
            _ = setResponse ?? throw new ArgumentNullException(nameof(setResponse));
            return builder.Configure(() => builder.HttpResponseMessage = setResponse(builder.HttpResponseMessage));
        }

        /// <summary>
        ///     Sets the <see cref="HttpResponseMessage"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="httpResponseMessage">
        ///     The new <see cref="HttpResponseMessage"/> to be used.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="httpResponseMessage"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetResponse<T>(this T builder, HttpResponseMessage httpResponseMessage)
            where T : IHttpResponseMessageBuilder
        {
            _ = httpResponseMessage ?? throw new ArgumentNullException(nameof(httpResponseMessage));
            return builder.Configure(() => builder.HttpResponseMessage = httpResponseMessage);
        }

    }

}
