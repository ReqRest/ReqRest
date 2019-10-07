namespace ReqRest.Serializers.Json
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using ReqRest;
    using ReqRest.Http;

    public static partial class JsonBuilderExtensions
    {

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(this ResponseTypeInfoBuilder<T> builder, params StatusCodeRange[] forStatusCodes) 
            where T : ApiRequestBase
        {
            return builder.AsJson((IEnumerable<StatusCodeRange>)forStatusCodes);
        }
        
        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(this ResponseTypeInfoBuilder<T> builder, IEnumerable<StatusCodeRange> forStatusCodes) 
            where T : ApiRequestBase
        {
            return builder.AsJson((Func<JsonHttpContentSerializer>?)null, forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializerOptions">
        ///     The options which should be used for (de-)serializing objects
        ///     to and from JSON.
        ///     
        ///     This can be <see langword="null"/>. If so, default options will be used.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ResponseTypeInfoBuilder<T> builder,
            JsonSerializerOptions? jsonSerializerOptions,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return builder.AsJson(jsonSerializerOptions, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializerOptions">
        ///     The options which should be used for (de-)serializing objects
        ///     to and from JSON.
        ///     
        ///     This can be <see langword="null"/>. If so, default options will be used.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ResponseTypeInfoBuilder<T> builder,
            JsonSerializerOptions? jsonSerializerOptions,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return AsJson(builder, Factory, forStatusCodes);

            JsonHttpContentSerializer Factory() => 
                new JsonHttpContentSerializer(jsonSerializerOptions);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonHttpContentDeserializerFactory">
        ///     A factory function which creates a <see cref="JsonHttpContentSerializer"/>
        ///     which should be used for the deserialization.
        ///     
        ///     This can be <see langword="null"/>.
        ///     If so, a factory returning a default <see cref="JsonHttpContentSerializer"/> will be used.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ResponseTypeInfoBuilder<T> builder,
            Func<JsonHttpContentSerializer>? jsonHttpContentDeserializerFactory,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return builder.AsJson(jsonHttpContentDeserializerFactory, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonHttpContentDeserializerFactory">
        ///     A factory function which creates a <see cref="JsonHttpContentSerializer"/>
        ///     which should be used for the deserialization.
        ///     
        ///     This can be <see langword="null"/>.
        ///     If so, a factory returning a default <see cref="JsonHttpContentSerializer"/> will be used.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ResponseTypeInfoBuilder<T> builder,
            Func<JsonHttpContentSerializer>? jsonHttpContentDeserializerFactory,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = forStatusCodes ?? throw new ArgumentNullException(nameof(forStatusCodes));
            jsonHttpContentDeserializerFactory ??= JsonHttpContentSerializer.DefaultFactory;

            return builder.Build(
                jsonHttpContentDeserializerFactory,
                forStatusCodes
            );
        }

    }

}
