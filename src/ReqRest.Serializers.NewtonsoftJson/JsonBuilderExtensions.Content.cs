namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.Net.Http;
    using System.Text;
    using ReqRest.Builders;

    /// <summary>
    ///     Defines various methods extending the builders for interacting with JSON.
    /// </summary>
    public static partial class JsonBuilderExtensions
    {

        /// <summary>
        ///     Sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="TContent">The type of the content to be serialized.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static TBuilder SetJsonContent<TBuilder, TContent>(
            this TBuilder builder,
            TContent content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null) where TBuilder : IHttpContentBuilder
        {
            return builder.SetContent(
                serializer ?? JsonHttpContentSerializer.Default,
                content,
                encoding
            );
        }

        /// <summary>
        ///     Sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
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
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="content"/> is not a subclass of <paramref name="contentType"/>, i.e.
        ///     the two types do not match.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static T SetJsonContent<T>(
            this T builder,
            object? content,
            Type? contentType,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null) where T : IHttpContentBuilder
        {
            return builder.SetContent(
                serializer ?? JsonHttpContentSerializer.Default, 
                content, 
                contentType, 
                encoding
            );
        }

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>POST</c> method and then
        ///     sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="TContent">The type of the content to be serialized.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <remarks>
        ///     Calling this method is equivalent to calling <see cref="HttpMethodBuilderExtensions.Post"/>
        ///     and <see cref="SetJsonContent{TBuilder, TContent}(TBuilder, TContent, Encoding?, JsonHttpContentSerializer?)"/>
        ///     in order.
        /// </remarks>
        public static TBuilder PostJson<TBuilder, TContent>(
            this TBuilder builder,
            TContent content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null)
            where TBuilder : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Post().SetJsonContent(content, encoding, serializer);
        }

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>POST</c> method and then
        ///     sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
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
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <remarks>
        ///     Calling this method is equivalent to calling <see cref="HttpMethodBuilderExtensions.Post"/>
        ///     and <see cref="SetJsonContent{T}(T, object?, Type?, Encoding?, JsonHttpContentSerializer?)"/>
        ///     in order.
        /// </remarks>
        public static T PostJson<T>(
            this T builder,
            object? content,
            Type? contentType,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null)
            where T : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Post().SetJsonContent(content, contentType, encoding, serializer);
        }

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>PUT</c> method and then
        ///     sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="TContent">The type of the content to be serialized.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <remarks>
        ///     Calling this method is equivalent to calling <see cref="HttpMethodBuilderExtensions.Post"/>
        ///     and <see cref="SetJsonContent{TBuilder, TContent}(TBuilder, TContent, Encoding?, JsonHttpContentSerializer?)"/>
        ///     in order.
        /// </remarks>
        public static TBuilder PutJson<TBuilder, TContent>(
            this TBuilder builder,
            TContent content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null)
            where TBuilder : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Put().SetJsonContent(content, encoding, serializer);
        }
        
        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>PUT</c> method and then
        ///     sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
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
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <remarks>
        ///     Calling this method is equivalent to calling <see cref="HttpMethodBuilderExtensions.Post"/>
        ///     and <see cref="SetJsonContent{T}(T, object?, Type?, Encoding?, JsonHttpContentSerializer?)"/>
        ///     in order.
        /// </remarks>
        public static T PutJson<T>(
            this T builder,
            object? content,
            Type? contentType,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null)
            where T : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Put().SetJsonContent(content, contentType, encoding, serializer);
        }

        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>PATCH</c> method and then
        ///     sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="TContent">The type of the content to be serialized.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <remarks>
        ///     Calling this method is equivalent to calling <see cref="HttpMethodBuilderExtensions.Post"/>
        ///     and <see cref="SetJsonContent{TBuilder, TContent}(TBuilder, TContent, Encoding?, JsonHttpContentSerializer?)"/>
        ///     in order.
        /// </remarks>
        public static TBuilder PatchJson<TBuilder, TContent>(
            this TBuilder builder,
            TContent content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null)
            where TBuilder : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Patch().SetJsonContent(content, encoding, serializer);
        }
        
        /// <summary>
        ///     Sets the <see cref="HttpMethod"/> to the HTTP <c>PATCH</c> method and then
        ///     sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
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
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <remarks>
        ///     Calling this method is equivalent to calling <see cref="HttpMethodBuilderExtensions.Post"/>
        ///     and <see cref="SetJsonContent{T}(T, object?, Type?, Encoding?, JsonHttpContentSerializer?)"/>
        ///     in order.
        /// </remarks>
        public static T PatchJson<T>(
            this T builder,
            object? content,
            Type? contentType,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null)
            where T : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Patch().SetJsonContent(content, contentType, encoding, serializer);
        }

    }

}
