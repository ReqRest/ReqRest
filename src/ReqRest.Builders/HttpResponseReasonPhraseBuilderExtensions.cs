namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpResponseReasonPhraseBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpResponseReasonPhraseBuilderExtensions
    {

        /// <summary>
        ///     Sets the reason phrase of the HTTP message which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="reasonPhrase">The reason phrase.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetReasonPhrase<T>(this T builder, string? reasonPhrase) where T : IHttpResponseReasonPhraseBuilder =>
            builder.Configure(builder => builder.ReasonPhrase = reasonPhrase);

    }

}
