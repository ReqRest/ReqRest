namespace ReqRest.Serializers
{
    using System.Net.Http;
    using System.Text;

    /// <summary>
    ///     Represents an element which is able to serialize .NET objects into
    ///     <see cref="HttpContent"/> instances.
    ///     
    ///     Consider deriving from <see cref="HttpContentSerializer"/> instead of implementing
    ///     this interface directly.
    /// </summary>
    public interface IHttpContentSerializer
    {

        /// <summary>
        ///     Serializes the specified <paramref name="content"/> into a new
        ///     <see cref="HttpContent"/> instance.
        /// </summary>
        /// <param name="content">
        ///     The object to be serialized into a new <see cref="HttpContent"/> instance.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="encoding">
        ///     An optional encoding to be used by the serializer if it serializes the <paramref name="content"/>
        ///     to an <see cref="HttpContent"/> which requires one.
        ///     If <see langword="null"/>, a default encoding is used.
        /// </param>
        /// <returns>
        ///     A new <see cref="HttpContent"/> instance which holds the serialized <paramref name="content"/>.
        /// </returns>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        HttpContent Serialize(object? content, Encoding? encoding);

    }

}
