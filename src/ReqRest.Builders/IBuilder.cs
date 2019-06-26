namespace ReqRest
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

    internal static class BuilderExtensions
    {

        /// <summary>
        ///     Verifies that <paramref name="builder"/> is not <see langword="null"/> and then
        ///     calls <paramref name="configure"/> which should do the actual building task.
        ///     Then returns the same <paramref name="builder"/> for fluent method chaining.
        /// </summary>
        [DebuggerStepThrough]
        internal static T Configure<T>(this T builder, Action configure) where T : IBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            configure();
            return builder;
        }

        /// <summary>
        ///     Executes the specified <paramref name="configure"/> function to configure or modify
        ///     this builder instance.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
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
        public static T Configure<T>(this T builder, Action<T> configure) where T : IBuilder
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            _ = configure ?? throw new ArgumentNullException(nameof(configure));

            configure(builder);
            return builder;
        }

    }

}
