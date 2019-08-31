namespace ReqRest.Serializers
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using ReqRest.Serializers.Resources;

    /// <summary>
    ///     An abstract base class for implementers of the <see cref="IHttpContentSerializer"/> and
    ///     <see cref="IHttpContentDeserializer"/> interfaces.
    ///     
    ///     This class takes care of the boilerplate code that should be implemented for these
    ///     two interfaces.
    ///     See remarks for details.
    /// </summary>
    /// <remarks>
    ///     The <see cref="IHttpContentSerializer"/> and <see cref="IHttpContentDeserializer"/>
    ///     interfaces document certain behaviors which every implementer should fulfill.
    ///     This base class already implements said features, leaving you with only having
    ///     to implement the actual serialization logic in your custom serializer.
    ///     
    ///     These features are already implemented:
    ///     <list type="bullet">
    ///         <item>Parameter validation.</item>
    ///         <item>
    ///             Throwing an <see cref="HttpContentSerializationException"/> if 
    ///             (de-)serialization fails.
    ///         </item>
    ///         <item>
    ///             Handling the <see cref="NoContent"/> type during (de-)serialization.
    ///         </item>
    ///     </list>
    /// </remarks>
    public abstract class HttpContentSerializer : IHttpContentSerializer, IHttpContentDeserializer
    {

        /// <summary>
        ///     Gets the default <see cref="Encoding"/> which is passed to
        ///     <see cref="SerializeCore(object, Encoding)"/> if no encoding was provided by
        ///     the user.
        ///     If not overridden, this is <see cref="Encoding.UTF8"/>.
        /// </summary>
        protected virtual Encoding DefaultEncoding => Encoding.UTF8;

        /// <inheritdoc/>
        public virtual HttpContent Serialize(object? content, Encoding? encoding)
        {
            if (content is NoContent)
            {
                return new ByteArrayContent(Array.Empty<byte>());
            }
            else
            {
                return SerializeDefault(content, encoding);
            }
        }

        private HttpContent SerializeDefault(object? content, Encoding? encoding)
        {
            try
            {
                return SerializeCore(content, encoding ?? DefaultEncoding);
            }
            catch (Exception ex) when (!(ex is HttpContentSerializationException))
            {
                throw new HttpContentSerializationException(null, ex);
            }
        }

        /// <summary>
        ///     Called by <see cref="Serialize(object, Encoding?)"/>.
        ///     This method should perform the actual serialization logic, i.e. it should
        ///     serialize the specified <paramref name="content"/> into a new
        ///     <see cref="HttpContent"/> instance.
        /// </summary>
        /// <param name="content">
        ///     The object to be serialized into a new <see cref="HttpContent"/> instance.
        /// </param>
        /// <param name="encoding">
        ///     An optional encoding to be used by the serializer if it serializes the <paramref name="content"/>
        ///     to an <see cref="HttpContent"/> which requires one.
        /// </param>
        /// <returns>
        ///     A new <see cref="HttpContent"/> instance which holds the serialized <paramref name="content"/>.
        /// </returns>
        protected abstract HttpContent SerializeCore(object? content, Encoding encoding);

        /// <inheritdoc/>
        public virtual async Task<object?> DeserializeAsync(HttpContent? httpContent, Type contentType)
        {
            _ = contentType ?? throw new ArgumentNullException(nameof(contentType));

            if (contentType == typeof(NoContent))
            {
                return new NoContent();
            }
            else
            {
                return await DeserializeDefault(httpContent, contentType).ConfigureAwait(false);
            }
        }

        private async Task<object?> DeserializeDefault(HttpContent? httpContent, Type contentType)
        {
            // We are not expecting NoContent at this point. This means that an empty HttpContent
            // (i.e. null) should not be legal.
            if (httpContent is null)
            {
                throw new HttpContentSerializationException(
                    ExceptionStrings.HttpContentSerializer_HttpContentIsNullButShouldNotBeNoContent(contentType)
                );
            }
            
            try
            {
                return await DeserializeCore(httpContent, contentType).ConfigureAwait(false);
            }
            catch (Exception ex) when (!(ex is HttpContentSerializationException))
            {
                throw new HttpContentSerializationException(null, ex);
            }
        }

        /// <summary>
        ///     Called by <see cref="DeserializeAsync(HttpContent, Type)"/>.
        ///     This method should perform the actual deserialization logic, i.e. it should
        ///     deserialize an object of the specified <paramref name="contentType"/> from
        ///     the <paramref name="httpContent"/>.
        /// </summary>
        /// <param name="httpContent">
        ///     An <see cref="HttpContent"/> instance from which the content should be serialized.
        /// </param>
        /// <param name="contentType">
        ///     The target type of the object which is supposed to be deserialized.
        /// </param>
        /// <returns>
        ///     An object of type <paramref name="contentType"/>.
        /// </returns>
        protected abstract Task<object?> DeserializeCore(HttpContent httpContent, Type contentType);

    }

}
