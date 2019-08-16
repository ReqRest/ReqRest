namespace ReqRest.Serializers.NewtonsoftJson
{
    using System.Text;
    using System.Net.Http;
    using ReqRest.Builders;

    /// <summary>
    ///     Provides static builder extension methods dealing with the HTTP method and JSON.
    /// </summary>
    public static class JsonHttpMethodBuilderExtensions
    {

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
        ///     and <see cref="JsonHttpContentBuilderExtensions.SetJsonContent{T}(T, object, Encoding, JsonHttpContentSerializer)"/>
        ///     in order.
        /// </remarks>
        public static T PostJson<T>(
            this T builder, object? content, Encoding? encoding = null, JsonHttpContentSerializer? serializer = null)
            where T : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Post().SetJsonContent(content, encoding, serializer);
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
        ///     and <see cref="JsonHttpContentBuilderExtensions.SetJsonContent{T}(T, object, Encoding, JsonHttpContentSerializer)"/>
        ///     in order.
        /// </remarks>
        public static T PutJson<T>(
            this T builder, object? content, Encoding? encoding = null, JsonHttpContentSerializer? serializer = null)
            where T : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Put().SetJsonContent(content, encoding, serializer);
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
        ///     and <see cref="JsonHttpContentBuilderExtensions.SetJsonContent{T}(T, object, Encoding, JsonHttpContentSerializer)"/>
        ///     in order.
        /// </remarks>
        public static T PatchJson<T>(
            this T builder, object? content, Encoding? encoding = null, JsonHttpContentSerializer? serializer = null)
            where T : IHttpMethodBuilder, IHttpContentBuilder
        {
            return builder.Patch().SetJsonContent(content, encoding, serializer);
        }
        
    }

}
