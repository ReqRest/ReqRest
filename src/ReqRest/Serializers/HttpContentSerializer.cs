namespace ReqRest.Serializers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ReqRest.Http;
    using ReqRest.Resources;

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
        ///     <see cref="SerializeCore(object?, Type?, Encoding)"/> if no encoding was provided by
        ///     the user.
        ///     If not overridden, this is <see cref="Encoding.UTF8"/>.
        /// </summary>
        protected virtual Encoding DefaultEncoding => Encoding.UTF8;

        /// <inheritdoc/>
        public virtual HttpContent? Serialize(object? content, Type? contentType, Encoding? encoding)
        {
            contentType = GetAndVerifyContentType(content, contentType);

            if (content is NoContent || contentType == typeof(NoContent))
            {
                return null;
            }

            try
            {
                return SerializeCore(content, contentType, encoding ?? DefaultEncoding);
            }
            catch (Exception ex) when (!(ex is HttpContentSerializationException))
            {
                throw new HttpContentSerializationException(null, ex);
            }
        }

        private static Type? GetAndVerifyContentType(object? content, Type? contentType)
        {
            // If both types are given, ensure that they match. Otherwise serialization will be problematic.
            var actualContentType = content?.GetType();
            if (actualContentType != null && contentType != null && !contentType.IsAssignableFrom(actualContentType))
            {
                throw new ArgumentException(
                    ExceptionStrings.HttpContentSerializer_ContentTypeDoesNotMatchActualType(contentType, actualContentType),
                    nameof(contentType)
                );
            }

            // The contentType is optional. In that case, try to get the type on our own.
            return contentType ?? actualContentType;
        }

        /// <summary>
        ///     Called by <see cref="Serialize(object?, Type?, Encoding?)"/>.
        ///     This method should perform the actual serialization logic, i.e. it should
        ///     serialize the specified <paramref name="content"/> into a new
        ///     <see cref="HttpContent"/> instance.
        /// </summary>
        /// <param name="content">
        ///     The object to be serialized into a new <see cref="HttpContent"/> instance.
        /// </param>
        /// <param name="contentType">
        ///     The type of the specified <paramref name="content"/>.
        ///     
        ///     This is can be <see langword="null"/> if <paramref name="content"/> is also <see langword="null"/>
        ///     and if this was not specified by the caller.
        ///     
        ///     It this is not <see langword="null"/>, it is guaranteed to be a type that <paramref name="content"/>
        ///     can be assigned to, i.e. <paramref name="content"/> has either the same type, or it
        ///     has a type deriving from this parameter.
        /// </param>
        /// <param name="encoding">
        ///     An optional encoding to be used by the serializer if it serializes the <paramref name="content"/>
        ///     to an <see cref="HttpContent"/> which requires one.
        /// </param>
        /// <returns>
        ///     A new <see cref="HttpContent"/> instance which holds the serialized <paramref name="content"/>
        ///     or <see langword="null"/>.
        /// </returns>
        protected abstract HttpContent? SerializeCore(object? content, Type? contentType, Encoding encoding);

        /// <inheritdoc/>
        public virtual async Task<object?> DeserializeAsync(
            HttpContent? httpContent,
            Type contentType,
            CancellationToken cancellationToken = default)
        {
            _ = contentType ?? throw new ArgumentNullException(nameof(contentType));

            if (contentType == typeof(NoContent))
            {
                return new NoContent();
            }

            try
            {
                // An HttpContent which is null is expected to be hard to interpret for deserializers.
                // For convenience, default handling of that case is added in this base class.
                // By default, this simply throws. This can be overridden though.
                if (httpContent is null)
                {
                    return await DeserializeWithoutHttpContentAsync(contentType, cancellationToken).ConfigureAwait(false);
                }
                return await DeserializeAsyncCore(httpContent, contentType, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex) when (!(ex is HttpContentSerializationException))
            {
                throw new HttpContentSerializationException(null, ex);
            }
        }

        /// <summary>
        ///     Called by <see cref="DeserializeAsync(HttpContent, Type, CancellationToken)"/>
        ///     when the <see cref="HttpContent"/> to be deserialized is <see langword="null"/>.
        ///     
        ///     If not overridden, this method throws an <see cref="HttpContentSerializationException"/>
        ///     indicating that deserializing an <see cref="HttpContent"/> is only possible if
        ///     the <paramref name="contentType"/> is the type of the <see cref="NoContent"/> class.
        ///     
        ///     Override this method if your serializer supports creating default values of a type
        ///     without having any HTTP content.
        /// </summary>
        /// <param name="contentType">
        ///     The target type of the object which is supposed to be deserialized.
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     If overridden, return an object of type <paramref name="contentType"/>.
        /// </returns>
        /// <exception cref="HttpContentSerializationException">
        ///     This exception gets thrown if the method is not overridden by deriving classes.
        /// </exception>
        [DoesNotReturn]
        protected virtual Task<object?> DeserializeWithoutHttpContentAsync(Type contentType, CancellationToken cancellationToken)
        {
            throw new HttpContentSerializationException(
                ExceptionStrings.HttpContentSerializer_HttpContentIsNull(contentType)
            );
        }

        /// <summary>
        ///     Called by <see cref="DeserializeAsync(HttpContent, Type, CancellationToken)"/>.
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
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An object of type <paramref name="contentType"/>.
        /// </returns>
        protected abstract Task<object?> DeserializeAsyncCore(
            HttpContent httpContent,
            Type contentType,
            CancellationToken cancellationToken
        );

    }

}
