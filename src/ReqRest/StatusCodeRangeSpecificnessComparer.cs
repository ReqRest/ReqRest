namespace ReqRest
{
    using System.Collections.Generic;
    using ReqRest.Http;

    /// <summary>
    ///     A special comparer for <see cref="StatusCodeRange"/> instances which orders them
    ///     by their specificness, i.e. which status code range describes a "tighter" area 
    ///     of status codes.
    /// </summary>
    internal sealed class StatusCodeRangeSpecificnessComparer : Comparer<StatusCodeRange>
    {

        private const int LessThan = -1;
        private const int Equal = 0;
        private const int GreaterThan = 1;

        /// <summary>
        ///     Gets a default <see cref="StatusCodeRangeSpecificnessComparer"/> instance.
        /// </summary>
        public static new StatusCodeRangeSpecificnessComparer Default { get; }
            = new StatusCodeRangeSpecificnessComparer();

        /// <summary>
        ///     Returns a value indicating if <paramref name="x"/> is more specific than
        ///     <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first range.</param>
        /// <param name="y">The second range.</param>
        public override int Compare(StatusCodeRange x, StatusCodeRange y)
        {
            if (x.IsMoreSpecificThan(y))
            {
                return GreaterThan;
            }
            else if (y.IsMoreSpecificThan(x))
            {
                return LessThan;
            }
            else
            {
                return Equal;
            }
        }

    }

}
