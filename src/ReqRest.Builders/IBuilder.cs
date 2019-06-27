namespace ReqRest.Builders
{
    using System;
    using System.Diagnostics;

#pragma warning disable CA1040 // Avoid empty interfaces

    /// <summary>
    ///     Used to give a single entry point for extension methods that should be
    ///     available for every single builder defined by this library.
    /// </summary>
    public interface IBuilder { }

#pragma warning restore CA1040 // Avoid empty interfaces

    /// <summary>
    ///     Provides extension methods that are available for every single <see cref="IBuilder"/>
    ///     provided by the library.
    /// </summary>
    public static class BuilderExtensions
    {

        /// <summary>
        ///     Executes the specified <paramref name="configure"/> function to configure or modify
        ///     this builder instance.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="configure">
        ///     A function which receives this builder instance as its parameter.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="configure"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T Configure<T>(this T builder, Action<T> configure) where T : IBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            configure(builder);
            return builder;
        }

        /// <summary>
        ///     Executes the specified <paramref name="action"/> to configure or modify this
        ///     builder instance, but only if the <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="condition">
        ///     A condition which must be <see langword="true"/> for the <paramref name="action"/> to
        ///     be executed.
        /// </param>
        /// <param name="action">
        ///     An action to be executed if the <paramref name="condition"/> is <see langword="true"/>.
        ///     This action receives the this builder instance as its parameter.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="action"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T If<T>(this T builder, bool condition, Action<T> action) where T : IBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = action ?? throw new ArgumentNullException(nameof(action));

            if (condition)
            {
                action(builder);
            }

            return builder;
        }

        /// <summary>
        ///     Executes the specified <paramref name="action"/> to configure or modify this
        ///     builder instance, but only if the <paramref name="condition"/> is <see langword="false"/>.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="condition">
        ///     A condition which must be <see langword="false"/> for the <paramref name="action"/> to
        ///     be executed.
        /// </param>
        /// <param name="action">
        ///     An action to be executed if the <paramref name="condition"/> is <see langword="false"/>.
        ///     This action receives the this builder instance as its parameter.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        ///     * <paramref name="action"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T IfNot<T>(this T builder, bool condition, Action<T> action) where T : IBuilder =>
            builder.If(!condition, action);

    }

}
