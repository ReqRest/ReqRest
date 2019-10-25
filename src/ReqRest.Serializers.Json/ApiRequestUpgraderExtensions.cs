namespace ReqRest.Serializers.Json
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializerOptions? jsonSerializerOptions,
            params StatusCodeRange[] forStatusCodes)
            where T : ApiRequestBase
        {
            return requestUpgrader.AsJson(jsonSerializerOptions, (IEnumerable<StatusCodeRange>)forStatusCodes);
        }

        /// <summary>
        ///     Declares that an object returned by the API should be deserialized from JSON if the
        ///     response falls within one of the specified status code ranges.
        /// </summary>
        /// <typeparam name="T">The request.</typeparam>
        /// <param name="requestUpgrader">The request upgrader.</param>
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
        ///     * <paramref name="requestUpgrader"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public static T AsJson<T>(
            this ApiRequestUpgrader<T> requestUpgrader,
            JsonSerializerOptions? jsonSerializerOptions,
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            return AsJson(requestUpgrader, Provider, forStatusCodes);

            JsonHttpContentSerializer Provider() => 
                new JsonHttpContentSerializer(jsonSerializerOptions);
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
            return requestUpgrader.AsJson(jsonHttpContentDeserializerProvider, (IEnumerable<StatusCodeRange>)forStatusCodes);
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
