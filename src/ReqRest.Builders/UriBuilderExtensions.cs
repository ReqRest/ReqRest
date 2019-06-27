namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;

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
            builder.Configure(_ => builder.Scheme = scheme);

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
            builder.Configure(_ => builder.UserName = userName);

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
            builder.Configure(_ => builder.Password = password);

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
            builder.Configure(_ => builder.Host = host);

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
            builder.Configure(_ => builder.Port = port ?? -1);

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
            builder.Configure(_ => builder.Path = path);

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
            builder.Configure(_ => builder.Query = query);

        /// <summary>
        ///     Sets the <see cref="UriBuilder.Fragment"/> property to the specified
        ///     <paramref name="fragment"/> and returns the same builder instance.
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
            builder.Configure(_ => builder.Fragment = fragment);

        /// <summary>
        ///     Appends the specified path <paramref name="segment"/> to the builder's
        ///     <see cref="UriBuilder.Path"/> and returns the same builder instance.
        /// </summary>
        /// <typeparam name="T">The <see cref="UriBuilder"/> type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="segment">
        ///     The segment to be appended to the builder's path.
        ///     
        ///     If <see langword="null"/> or empty, a single slash is appended instead,
        ///     resulting in a double slash <c>//</c>.
        /// </param>
        /// <returns>The same <see cref="UriBuilder"/> instance.</returns>
        [DebuggerStepThrough]
        public static T AppendPath<T>(this T builder, string? segment) where T : UriBuilder
        {
            var path = builder.Path;
            
            if (!path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                path += "/";
            }

            // If the segment starts with a single /, that may be unintended.
            // In this case, strip the leading /.
            // If the segment is only a single slash, or multiple in a row, that is considered
            // intentional.
            // In this case, simply keep these slashes.
            if (segment != null && segment.Length > 1 && segment[0] == '/' && segment[1] != '/')
            {
                segment = segment.TrimStart(new char[] { '/' });
            }

            if (string.IsNullOrEmpty(segment))
            {
                segment = "/";
            }

            return builder.SetPath(path + segment);
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
        public static T Configure<T>(this T builder, Action<UriBuilder> configure) where T : UriBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            configure(builder);
            return builder;
        }

    }

}
