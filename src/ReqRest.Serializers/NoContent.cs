namespace ReqRest.Serializers
{
    using System;

    /// <summary>
    ///     A special type which should be used to represent an empty content of an HTTP response.
    ///     This class is treated with special logic during (de-)serialization and is thus the preferred
    ///     way for representing an empty HTTP response content.
    ///     
    ///     Use <see cref="Default"/> for accessing an instance of this class.
    /// </summary>
    [Serializable]
    public struct NoContent : IEquatable<NoContent>
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
        ///     Returns a value indicating whether this object equals <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The object to be compared with this object.</param>
        /// <returns>
        ///     <see langword="true"/>.
        /// </returns>
        public bool Equals(NoContent other) =>
            true;

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A constant hash code.</returns>
        public override int GetHashCode() =>
            1;

        public static bool operator ==(NoContent? a, NoContent? b) =>
            Equals(a, b);

        public static bool operator !=(NoContent? a, NoContent? b) =>
            !(a == b);

    }

}
