namespace ReqRest.Http
{
    using System;

    /// <summary>
    ///     A special type which is used by to represent an empty HTTP message content, for example
    ///     when an API returns `204 No Content`.
    /// </summary>
    /// <remarks>
    ///     This class is treated with special logic during (de-)serialization and is thus the preferred
    ///     way for representing an empty HTTP response content within the context of this library.
    /// </remarks>
    [Serializable]
    public readonly struct NoContent : IEquatable<NoContent>
    {

        /// <summary>
        ///     Returns a value indicating whether this object equals <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The object to be compared with this object.</param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="obj"/> is of type <see cref="NoContent"/>;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public override bool Equals(object obj) =>
            obj is NoContent;

        /// <summary>
        ///     Returns a value indicating whether this <see cref="NoContent"/> instance
        ///     equals <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The object to be compared with this object.</param>
        /// <returns>
        ///     This always returns <see langword="true"/>.
        /// </returns>
        public bool Equals(NoContent other) =>
            true;

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A constant hash code.</returns>
        public override int GetHashCode() =>
            1;

        /// <summary>
        ///     Returns a value indicating whether one <see cref="NoContent"/> instance
        ///     is equal to another one.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>
        ///     This always returns <see langword="true"/>.
        /// </returns>
        public static bool operator ==(NoContent a, NoContent b) =>
            Equals(a, b);

        /// <summary>
        ///     Returns a value indicating whether one <see cref="NoContent"/> instance
        ///     is unequal to another one.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>
        ///     This always returns <see langword="false"/>.
        /// </returns>
        public static bool operator !=(NoContent a, NoContent b) =>
            !(a == b);

    }

}
