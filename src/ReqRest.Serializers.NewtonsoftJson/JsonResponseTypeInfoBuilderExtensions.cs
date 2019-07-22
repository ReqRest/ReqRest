namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ReqRest.Client;
    using ReqRest.Http;

    /// <summary>
    ///     Extends the <see cref="ResponseTypeInfoBuilder{TRequest}"/> class with methods for
    ///     declaring that a certain response type will be received as JSON.
    /// </summary>
    public static partial class JsonResponseTypeInfoBuilderExtensions
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
        ///     An generic <see cref="ApiRequest"/> variation.
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
        /// <param name="jsonSerializer">
        ///     A <see cref="JsonSerializer"/> which should be used for the deserialization.
        /// </param>
        /// <returns>
        ///     An generic <see cref="ApiRequest"/> variation.
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
            IEnumerable<StatusCodeRange> forStatusCodes,
            JsonSerializer? jsonSerializer = null)
            where T : ApiRequestBase
        {
            var factory = BuildDeserializerFactory(jsonSerializer);
            return AsJson(builder, forStatusCodes, factory);
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
        /// <param name="jsonHttpContentDeserializerFactory">
        ///     A factory function which creates a <see cref="JsonHttpContentSerializer"/>
        ///     which should be used for the deserialization.
        /// </param>
        /// <returns>
        ///     An generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="forStatusCodes"/>
        ///     * <paramref name="jsonHttpContentDeserializerFactory"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ResponseTypeInfoBuilder<T> builder, 
            IEnumerable<StatusCodeRange> forStatusCodes,
            Func<JsonHttpContentSerializer> jsonHttpContentDeserializerFactory)
            where T : ApiRequestBase
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = forStatusCodes ?? throw new ArgumentNullException(nameof(forStatusCodes));
            _ = jsonHttpContentDeserializerFactory ?? throw new ArgumentNullException(nameof(jsonHttpContentDeserializerFactory));

            return builder.Build(
                jsonHttpContentDeserializerFactory,
                forStatusCodes
            );
        }

        private static Func<JsonHttpContentSerializer> BuildDeserializerFactory(JsonSerializer? jsonSerializer) =>
            () => JsonHttpContentSerializer.FromJsonSerializer(jsonSerializer);

    }

}
