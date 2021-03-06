﻿namespace ReqRest.Serializers
{
    using System;
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
        /// <returns>
        ///     A new <see cref="HttpContent"/> instance which holds the serialized <paramref name="content"/>
        ///     or <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="content"/> is not a subclass of <paramref name="contentType"/>, i.e.
        ///     the two types do not match.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        HttpContent? Serialize(object? content, Type? contentType, Encoding? encoding);

    }

}
