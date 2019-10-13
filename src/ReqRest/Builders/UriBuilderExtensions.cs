namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    ///     Extends the <see cref="UriBuilder"/> class with fluent extension methods.
    /// </summary>
    public static class UriBuilderExtensions
    {

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Scheme"/> property to the specified
        ///     <paramref name="scheme"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="scheme">
        ///     The scheme of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Scheme"/>
        ///     will be set to an empty string.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetScheme<T>(this T builder, string? scheme) where T : UriBuilder =>
            builder.Configure(builder => builder.Scheme = scheme);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.UserName"/> property to the specified
        ///     <paramref name="userName"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="userName">
        ///     The optional user name of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.UserName"/>
        ///     will be set to an empty string.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetUserName<T>(this T builder, string? userName) where T : UriBuilder =>
            builder.Configure(builder => builder.UserName = userName);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Password"/> property to the specified
        ///     <paramref name="password"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="password">
        ///     The optional password of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Password"/>
        ///     will be set to an empty string.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetPassword<T>(this T builder, string? password) where T : UriBuilder =>
            builder.Configure(builder => builder.Password = password);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Host"/> property to the specified
        ///     <paramref name="host"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="host">
        ///     The optional host of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Host"/>
        ///     will be set to an empty string.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetHost<T>(this T builder, string? host) where T : UriBuilder =>
            builder.Configure(builder => builder.Host = host);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Port"/> property to the specified
        ///     <paramref name="port"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="port">
        ///     The optional port of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Port"/>
        ///     will be set to -1, indicating that no port information is available.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetPort<T>(this T builder, int? port) where T : UriBuilder =>
            builder.Configure(builder => builder.Port = port ?? -1);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Path"/> property to the specified
        ///     <paramref name="path"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="path">
        ///     The path of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Path"/>
        ///     will be set to a single slash <c>/</c>.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetPath<T>(this T builder, string? path) where T : UriBuilder =>
            builder.Configure(builder => builder.Path = path);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Query"/> property to the specified
        ///     <paramref name="query"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="query">
        ///     The optional query of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Query"/>
        ///     will be set to an empty string.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetQuery<T>(this T builder, string? query) where T : UriBuilder =>
            builder.Configure(builder => builder.Query = query);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Fragment"/> property to the specified
        ///     <paramref name="fragment"/> and returns the same builder instance.
        ///     
        ///     Be aware that the fragment identifier (<c>"#"</c>) is automatically added to the
        ///     beginning of the fragment by .NET's <see cref="UriBuilder"/> class.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="fragment">
        ///     The optional fragment of the <see cref="Uri"/> which is being built.
        ///     
        ///     This can be <see langword="null"/>. If so, <see cref="UriBuilder.Fragment"/>
        ///     will be set to an empty string.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T SetFragment<T>(this T builder, string? fragment) where T : UriBuilder =>
            builder.Configure(builder => builder.Fragment = fragment);

        /// <summary>
        ///     Appends the specified path <paramref name="segment"/> to the builder's
        ///     <see cref="UriBuilder.Path"/> and returns the same builder instance.
        ///     
        ///     If the existing path ends with a single slash, or if the <paramref name="segment"/>
        ///     starts with a single slash, the slashes are stripped, so that the resulting path
        ///     only has a single slash between the two concatenated parts.
        ///     
        ///     If the existing path starts with multiple slashes, or if the <paramref name="segment"/> 
        ///     starts with multiple slashes, one slash is removed, but the rest are kept.
        ///     
        ///     If <paramref name="segment"/> is <see langword="null"/> or empty, this method
        ///     does not change the builder's current path.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="segment">
        ///     The segment to be appended to the builder's path.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T AppendPath<T>(this T builder, string? segment) where T : UriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            var path = builder.Path;

            if (string.IsNullOrEmpty(segment))
            {
                return builder;
            }
            else
            {
#nullable disable // TODO: Remove this once the compiler no longer emits warnings.
                if (segment.StartsWith("/", StringComparison.Ordinal))
                {
                    segment = segment.Substring(1, segment.Length - 1);
                }

                if (path.EndsWith("/", StringComparison.Ordinal))
                {
                    path = path.Substring(0, path.Length - 1);
                }

                return builder.SetPath($"{path}/{segment}");
#nullable restore
            }
        }

        /// <summary>
        ///     Formats the specified parameters consisting of a key and value into
        ///     a set of query parameters (similar to <c>&amp;key=value</c>) and appends them at the
        ///     end of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameters starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameters.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="parameters">
        ///     A set of query parameters consisting of a key and value which should 
        ///     be appended to the query.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AppendQueryParameter<T>(this T builder, params (string? Key, string? Value)[]? parameters)
            where T : UriBuilder =>
                builder.AppendQueryParameter((IEnumerable<(string?, string?)>?)parameters);

        /// <summary>
        ///     Formats the specified parameters consisting of a key and value into
        ///     a set of query parameters (similar to <c>&amp;key=value</c>) and appends them at the
        ///     end of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameters starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameters.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="parameters">
        ///     A set of query parameters consisting of a key and value which should 
        ///     be appended to the query.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AppendQueryParameter<T>(this T builder, params KeyValuePair<string?, string?>[]? parameters)
            where T : UriBuilder =>
                builder.AppendQueryParameter((IEnumerable<KeyValuePair<string?, string?>>?)parameters);

        /// <summary>
        ///     Formats the specified parameters consisting of a key and value into
        ///     a set of query parameters (similar to <c>&amp;key=value</c>) and appends them at the
        ///     end of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameters starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameters.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="parameters">
        ///     A set of query parameters consisting of a key and value which should 
        ///     be appended to the query.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AppendQueryParameter<T>(this T builder, IEnumerable<(string? Key, string? Value)>? parameters)
            where T : UriBuilder
        {
            return builder.AppendQueryParameter(
                parameters?.Select(p => new KeyValuePair<string?, string?>(p.Key, p.Value))
            );
        }

        /// <summary>
        ///     Formats the specified parameters consisting of a key and value into
        ///     a set of query parameters (similar to <c>&amp;key=value</c>) and appends them at the
        ///     end of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameters starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameters.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="parameters">
        ///     A set of query parameters consisting of a key and value which should 
        ///     be appended to the query.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AppendQueryParameter<T>(this T builder, IEnumerable<KeyValuePair<string?, string?>>? parameters)
            where T : UriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    builder.AppendQueryParameter(param.Key, param.Value);
                }
            }

            return builder;
        }

        /// <summary>
        ///     Formats the specified <paramref name="key"/> and <paramref name="value"/> into
        ///     a query parameter (similar to <c>&amp;key=value</c>) and appends it at the end
        ///     of the <see cref="UriBuilder.Query"/> string.
        ///     
        ///     If the query ends with or if the final parameter starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameter.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="key">
        ///     The key of the query parameter.
        ///     This can be <see langword="null"/>. If so, it is treated as an empty string.
        ///     
        ///     If both <paramref name="key"/> and <paramref name="value"/> are 
        ///     <see langword="null"/> or empty, nothing gets appended.
        /// </param>
        /// <param name="value">
        ///     The value of the query parameter.
        ///     This can be <see langword="null"/>. If so, it is treated as an empty string.
        ///     
        ///     If both <paramref name="key"/> and <paramref name="value"/> are 
        ///     <see langword="null"/> or empty, nothing gets appended.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AppendQueryParameter<T>(this T builder, string? key, string? value)
            where T : UriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(value))
            {
                return builder;
            }

            return builder.AppendQueryParameter($"{key}={value}");
        }

        /// <summary>
        ///     Appends the specified <paramref name="parameter"/> at the end of the
        ///     <see cref="UriBuilder.Query"/>.
        ///     
        ///     If the query ends with or if the <paramref name="parameter"/> starts with one or
        ///     more <c>"&amp;"</c> characters, they are trimmed, so that there is only a single
        ///     <c>"&amp;"</c> between the old query and the new parameter.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="parameter">
        ///     The query parameter to be appended to the query.
        ///     
        ///     This can be <see langword="null"/>. If so (or if empty), nothing gets appended.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T AppendQueryParameter<T>(this T builder, string? parameter)
            where T : UriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(parameter))
            {
                return builder;
            }

#nullable disable // TODO: Remove this once the compiler no longer emits warnings.
            var trimChars = new[] { '&' };
            var query = builder.Query.TrimEnd(trimChars);
            parameter = parameter.TrimStart(trimChars);

            if (query.Length == 0 || query == "?")
            {
                return builder.SetQuery(parameter);
            }
            else
            {
                return builder.SetQuery($"{query}&{parameter}");
            }
#nullable restore
        }

        /// <summary>
        ///     Executes the specified <paramref name="configure"/> function to configure or modify
        ///     this builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configure">
        ///     A function which receives this builder instance.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configure"/>
        /// </exception>
        [DebuggerStepThrough]
        private static T Configure<T>(this T builder, Action<UriBuilder> configure) where T : UriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            configure(builder);
            return builder;
        }

    }

}
