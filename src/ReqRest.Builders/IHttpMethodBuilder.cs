namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;

    /// <summary>
    ///     Represents a builder for an object which provides an <see cref="System.Net.Http.HttpMethod"/>.
    /// </summary>
    public interface IHttpMethodBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP method which the builder builds.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        HttpMethod Method { get; set; }

    }

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpMethodBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpMethodBuilderExtensions
    {

        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>GET</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Get<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Get);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>POST</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Post<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Post);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>PUT</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Put<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Put);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>DELETE</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Delete<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Delete);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>OPTIONS</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Options<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Options);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>TRACE</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Trace<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Trace);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>HEAD</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Head<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(HttpMethod.Head);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>PATCH</c> method.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Patch<T>(this T builder) where T : IHttpMethodBuilder =>
            builder.SetMethod(PatchMethod);

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="method">
        ///     A string from which an <see cref="HttpMethod"/> can be created.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="method"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetMethod<T>(this T builder, string method) where T : IHttpMethodBuilder =>
            builder.SetMethod(new HttpMethod(method ?? throw new ArgumentNullException(nameof(method))));

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="method">
        ///     The HTTP method used by the HTTP request message.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="method"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetMethod<T>(this T builder, HttpMethod method) where T : IHttpMethodBuilder =>
            builder.Configure(_ => builder.Method = method ?? throw new ArgumentNullException(nameof(method)));

    }

}
