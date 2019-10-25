namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ReqRest;
    using ReqRest.Http;

    /// <summary>
    ///     Defines extension methods for the <see cref="ApiRequestUpgrader{TUpgradedRequest}"/>
    ///     class which allow upgrading a request using the <see cref="JsonHttpContentSerializer"/>.
    /// </summary>
    public static class ApiRequestUpgraderExtensions
    {

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(this ApiRequestUpgrader<T> requestUpgrader, params StatusCodeRange[] forStatusCodes) 
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson((IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(this ApiRequestUpgrader<T> requestUpgrader, IEnumerable<StatusCodeRange> forStatusCodes) 
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson((Func<JsonHttpContentSerializer>?)null, forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializer? jsonSerializer,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson(jsonSerializer, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializer? jsonSerializer,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return AsJson(requestUpgrader, Provider, forStatusCodes);

            JsonHttpContentSerializer Provider() => 
                new JsonHttpContentSerializer(jsonSerializer);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
        /// <param name="jsonSerializerSettings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     These settings are not merged with the default settings defined by <see cref="JsonConvert.DefaultSettings"/>.
        ///     Use the <see cref="AsJson{T}(ApiRequestUpgrader{T}, JsonSerializerSettings, bool, StatusCodeRange[])"/>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializerSettings? jsonSerializerSettings,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson(jsonSerializerSettings, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
        /// <param name="jsonSerializerSettings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     These settings are not merged with the default settings defined by <see cref="JsonConvert.DefaultSettings"/>.
        ///     Use the <see cref="AsJson{T}(ApiRequestUpgrader{T}, JsonSerializerSettings, bool, IEnumerable{StatusCodeRange})"/>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializerSettings? jsonSerializerSettings,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson(jsonSerializerSettings, JsonHttpContentSerializer.UseDefaultSettings, forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializerSettings? jsonSerializerSettings,
            bool useDefaultSettings,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson(jsonSerializerSettings, useDefaultSettings, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializerSettings? jsonSerializerSettings,
            bool useDefaultSettings,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return AsJson(requestUpgrader, Provider, forStatusCodes);

            JsonHttpContentSerializer Provider() => 
                new JsonHttpContentSerializer(jsonSerializerSettings, useDefaultSettings);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
        /// <param name="jsonHttpContentDeserializerProvider">
        ///     A function which creates a <see cref="JsonHttpContentSerializer"/>
        ///     which should be used for the deserialization.
        ///     
        ///     This can be <see langword="null"/>.
        ///     If so, a function returning a default <see cref="JsonHttpContentSerializer"/> will be used.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            Func<JsonHttpContentSerializer>? jsonHttpContentDeserializerProvider,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson(
                jsonHttpContentDeserializerProvider, 
                (IEnumerable<StatusCodeRange>)forStatusCodes
            );
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
        /// <param name="jsonHttpContentDeserializerProvider">
        ///     A function which creates a <see cref="JsonHttpContentSerializer"/>
        ///     which should be used for the deserialization.
        ///     
        ///     This can be <see langword="null"/>.
        ///     If so, a function returning a default <see cref="JsonHttpContentSerializer"/> will be used.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     A generic <see cref="ApiRequest"/> variation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            Func<JsonHttpContentSerializer>? jsonHttpContentDeserializerProvider,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            _ = requestUpgrader ?? throw new ArgumentNullException(nameof(requestUpgrader));
            _ = forStatusCodes ?? throw new ArgumentNullException(nameof(forStatusCodes));
            jsonHttpContentDeserializerProvider ??= JsonHttpContentSerializer.DefaultProvider;

            return requestUpgrader.Upgrade(
                jsonHttpContentDeserializerProvider,
                forStatusCodes
            );
        }

    }

}
