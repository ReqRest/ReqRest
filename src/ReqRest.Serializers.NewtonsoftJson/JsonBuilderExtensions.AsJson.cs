namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
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
        /// <param name="jsonSerializer">
        ///     A <see cref="JsonSerializer"/> which should be used for the deserialization.
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
            JsonSerializer? jsonSerializer,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return builder.AsJson(jsonSerializer, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }
        
        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializer">
        ///     A <see cref="JsonSerializer"/> which should be used for the deserialization.
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
            JsonSerializer? jsonSerializer,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return AsJson(builder, Factory, forStatusCodes);

            JsonHttpContentSerializer Factory() => 
                new JsonHttpContentSerializer(jsonSerializer);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializerSettings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     These settings are not merged with the default settings defined by <see cref="JsonConvert.DefaultSettings"/>.
        ///     Use the <see cref="AsJson{T}(ResponseTypeInfoBuilder{T}, JsonSerializerSettings, bool, StatusCodeRange[])"/>
        ///     overload for changing this behavior.
        ///     
        ///     This can be <see langword="null"/>. If so, a default settings instance is used instead.
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
            JsonSerializerSettings? jsonSerializerSettings,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return builder.AsJson(jsonSerializerSettings, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializerSettings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     These settings are not merged with the default settings defined by <see cref="JsonConvert.DefaultSettings"/>.
        ///     Use the <see cref="AsJson{T}(ResponseTypeInfoBuilder{T}, JsonSerializerSettings, bool, IEnumerable{StatusCodeRange})"/>
        ///     overload for changing this behavior.
        ///     
        ///     This can be <see langword="null"/>. If so, a default settings instance is used instead.
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
            JsonSerializerSettings? jsonSerializerSettings,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return builder.AsJson(jsonSerializerSettings, JsonHttpContentSerializer.UseDefaultSettings, forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializerSettings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="useDefaultSettings">
        ///     If <see langword="true"/>, the serializer created from the settings will use default
        ///     settings from <see cref="JsonConvert.DefaultSettings"/>.
        ///     If <see langword="false"/>, that is not the case. 
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
            JsonSerializerSettings? jsonSerializerSettings,
            bool useDefaultSettings,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return builder.AsJson(jsonSerializerSettings, useDefaultSettings, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }
        
        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="jsonSerializerSettings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="useDefaultSettings">
        ///     If <see langword="true"/>, the serializer created from the settings will use default
        ///     settings from <see cref="JsonConvert.DefaultSettings"/>.
        ///     If <see langword="false"/>, that is not the case. 
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
            JsonSerializerSettings? jsonSerializerSettings,
            bool useDefaultSettings,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return AsJson(builder, Factory, forStatusCodes);

            JsonHttpContentSerializer Factory() => 
                new JsonHttpContentSerializer(jsonSerializerSettings, useDefaultSettings);
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
