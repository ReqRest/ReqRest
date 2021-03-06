﻿namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using ReqRest.Serializers;

    /// <summary>
    ///     Defines the static methods for an <see cref="IHttpContentBuilder"/> provided
    ///     by the library.
    /// </summary>
    public static class HttpContentBuilderExtensions
    {

        #region SetFormUrlEncodedContent

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="FormUrlEncodedContent"/> instance which is
        ///     created from the specified <paramref name="content"/> pairs.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">
        ///     A set of key/value pairs which make up the form encoded HTTP content.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetFormUrlEncodedContent<T>(this T builder, params (string Key, string Value)[] content) 
            where T : IHttpContentBuilder =>
                builder.SetFormUrlEncodedContent((IEnumerable<(string, string)>)content);

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="FormUrlEncodedContent"/> instance which is
        ///     created from the specified <paramref name="content"/> pairs.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">
        ///     A set of key/value pairs which make up the form encoded HTTP content.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetFormUrlEncodedContent<T>(
            this T builder, IEnumerable<(string Key, string Value)> content) where T : IHttpContentBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = content ?? throw new ArgumentNullException(nameof(content));
            return builder.SetFormUrlEncodedContent(
                content.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
            );
        }

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="FormUrlEncodedContent"/> instance which is
        ///     created from the specified <paramref name="content"/> pairs.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">
        ///     A set of key/value pairs which make up the form encoded HTTP content.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetFormUrlEncodedContent<T>(this T builder, params KeyValuePair<string, string>[] content) 
            where T : IHttpContentBuilder =>
                builder.SetFormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)content);

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="FormUrlEncodedContent"/> instance which is
        ///     created from the specified <paramref name="content"/> pairs.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">
        ///     A set of key/value pairs which make up the form encoded HTTP content.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetFormUrlEncodedContent<T>(
            this T builder, IEnumerable<KeyValuePair<string, string>> content) where T : IHttpContentBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = content ?? throw new ArgumentNullException(nameof(content));
            return builder.SetContent(new FormUrlEncodedContent(content));
        }

        #endregion

        #region SetStringContent

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="StringContent"/> instance which is created
        ///     from the specified <paramref name="content"/> and <paramref name="encoding"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The new HTTP content as a string.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, the default value of the <see cref="StringContent"/>
        ///     class is used.
        /// </param>
        /// <param name="mediaType">
        ///     The media type used by the content.
        ///     If <see langword="null"/>, the default value of the <see cref="StringContent"/>
        ///     class is used.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetStringContent<T>(
            this T builder,
            string content,
            Encoding? encoding = null,
            string? mediaType = null) where T : IHttpContentBuilder
        {
#pragma warning disable CA2000
            _ = content ?? throw new ArgumentNullException(nameof(content));
            return builder.SetContent(new StringContent(content, encoding, mediaType));
#pragma warning restore CA2000
        }

        #endregion

        #region SetByteArrayContent

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="ByteArrayContent"/> instance which is
        ///     created from the specified <paramref name="content"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The new HTTP content as a byte array.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetByteArrayContent<T>(this T builder, byte[] content) where T : IHttpContentBuilder =>
            builder.SetByteArrayContent(content, 0, content?.Length ?? 0);

        /// <summary>
        ///     Sets the HTTP content to a new <see cref="ByteArrayContent"/> instance which is
        ///     created from the specified <paramref name="content"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">The new HTTP content as a byte array.</param>
        /// <param name="offset">A zero-based offset in <paramref name="content"/> from which sending starts.</param>
        /// <param name="count">The number of bytes to be sent, starting from <paramref name="offset"/>.</param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="content"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetByteArrayContent<T>(this T builder, byte[] content, int offset, int count) where T : IHttpContentBuilder
        {
#pragma warning disable CA2000 
            _ = content ?? throw new ArgumentNullException(nameof(content));
            return builder.SetContent(new ByteArrayContent(content, offset, count));
#pragma warning restore CA2000 
        }

        #endregion

        #region SetContent

        /// <summary>
        ///     Serializes the specified <paramref name="content"/> to an <see cref="HttpContent"/>
        ///     using the specified <paramref name="serializer"/> and then sets the HTTP content
        ///     which is being built to that <see cref="HttpContent"/>.
        ///     
        ///     This method passes the type of the generic type parameter <typeparamref name="TContent"/>
        ///     as the content type to the serializer.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="TContent">The type of the content to be serialized.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="serializer">
        ///     The serializer to be used for serializing the specified <paramref name="content"/> to
        ///     an <see cref="HttpContent"/>.    
        /// </param>
        /// <param name="content">
        ///     The object to be serialized into a new <see cref="HttpContent"/> instance.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="encoding">
        ///     An optional encoding to be used by the serializer if it serializes the <paramref name="content"/>
        ///     to an <see cref="HttpContent"/> which requires one.
        ///     If <see langword="null"/>, a default encoding is used.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="serializer"/>
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static TBuilder SetContent<TBuilder, TContent>(
            this TBuilder builder,
            IHttpContentSerializer serializer,
            TContent content,
            Encoding? encoding = null) where TBuilder : IHttpContentBuilder
        {
            // Validate builder here already so that no unnecessary serialization is done.
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = serializer ?? throw new ArgumentNullException(nameof(serializer));

            // Don't call the SetContent(object, Type) overload on purpose.
            // We have the extension method for a generic serialize, so we should use it.
            // If the behavior ever changes, only the extension method will have to be updated.
            var httpContent = serializer.Serialize(content, encoding);
            return builder.SetContent(httpContent);
        }

        /// <summary>
        ///     Serializes the specified <paramref name="content"/> to an <see cref="HttpContent"/>
        ///     using the specified <paramref name="serializer"/> and then sets the HTTP content
        ///     which is being built to that <see cref="HttpContent"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="serializer">
        ///     The serializer to be used for serializing the specified <paramref name="content"/> to
        ///     an <see cref="HttpContent"/>.    
        /// </param>
        /// <param name="content">
        ///     The object to be serialized into a new <see cref="HttpContent"/> instance.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="contentType">
        ///     The type of the specified <paramref name="content"/>.
        ///     This can be declared to give the serializer additional information about how
        ///     <paramref name="content"/> should be serialized.
        ///     
        ///     This can be <see langword="null"/>. If so, the serializer will try to determine the
        ///     type on its own. If <paramref name="content"/> is also <see langword="null"/>, the
        ///     serializer will use default <see langword="null"/> value handling.
        /// </param>
        /// <param name="encoding">
        ///     An optional encoding to be used by the serializer if it serializes the <paramref name="content"/>
        ///     to an <see cref="HttpContent"/> which requires one.
        ///     If <see langword="null"/>, a default encoding is used.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="serializer"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="content"/> is not a subclass of <paramref name="contentType"/>, i.e.
        ///     the two types do not match.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static T SetContent<T>(
            this T builder,
            IHttpContentSerializer serializer,
            object? content,
            Type? contentType,
            Encoding? encoding = null) where T : IHttpContentBuilder
        {
            // Validate builder here already so that no unnecessary serialization is done.
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = serializer ?? throw new ArgumentNullException(nameof(serializer));

            var httpContent = serializer.Serialize(content, contentType, encoding);
            return builder.SetContent(httpContent);
        }

        /// <summary>
        ///     Sets the HTTP content which is being built.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="content">
        ///     The new HTTP content.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetContent<T>(this T builder, HttpContent? content) where T : IHttpContentBuilder =>
            builder.Configure(builder => builder.Content = content);

        #endregion

    }

}
