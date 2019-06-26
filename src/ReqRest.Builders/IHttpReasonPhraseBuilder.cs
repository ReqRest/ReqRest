﻿namespace ReqRest
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     Represents a builder for a reason phrase which typically gets sent by a server
    ///     within the context of an HTTP message.
    /// </summary>
    public interface IHttpResponseReasonPhraseBuilder : IBuilder
    {
        
        /// <summary>
        ///     Gets or sets the reason phrase which the builder builds.
        /// </summary>
        string? ReasonPhrase { get; set; }

    }

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
            builder.Configure(() => builder.ReasonPhrase = reasonPhrase);

    }

}
