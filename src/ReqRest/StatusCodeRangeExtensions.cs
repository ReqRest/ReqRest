namespace ReqRest
{
    using ReqRest.Http;
    using static ReqRest.Http.StatusCode;

    /// <summary>
    ///    Extends the <see cref="StatusCodeRange"/> struct with utility methods required by members
    ///    of this library.
    /// </summary>
    internal static class StatusCodeRangeExtensions
    {

        /// <summary>
        ///     Returns a value indicating whether this status code range conflicts with the
        ///     specified one.
        ///     Two ranges conflict with each other if there is no reasonable way to decide
        ///     which range matches a specific status code better.
        /// </summary>
        /// <param name="range">This status code range.</param>
        /// <param name="other">The status code range to be compared with this one.</param>
        /// <returns>
        ///     <see langword="true"/> if the two ranges conflict with each other;
        ///     <see langword="false"/> if not.
        /// </returns>
        public static bool ConflictsWith(this StatusCodeRange range, StatusCodeRange other)
        {
            return AreSame()
                || HaveStandardRangeConflicts()
                || HaveWildcardRangeConflict();

            bool AreSame() =>
                range == other;

            // Standard conflicts for ranges without wildcard components. Examples:
            // 
            // - (200, 205) and (205, 210) conflict
            // - (200, 210) and (205, 210) conflict
            //
            // - (200, 205) and (210, 220) don't conflict because they cover different ranges.
            // - (200, 210) and (205) don't conflict because (205) is more specific.
            // - (200, 300) and (250, 300) don't conflict, because (250, 300) is more specific.
            bool HaveStandardRangeConflicts() =>
                   !range.IsSingleStatusCode && !other.IsSingleStatusCode
                && !range.HasWildcardComponent && !other.HasWildcardComponent
                && !range.IsInRange(other) && !other.IsInRange(range)
                && range.From <= other.To && other.From <= range.To;

            // Wildcard conflicts for ranges with wildcards on opposite sides. Examples:
            // 
            // - (*, 200) and (200, *) conflict
            // - (200, *) and (*, 200) conflict
            // 
            // - (*, 200) and (*, 205) don't conflict because (*, 200) is more specific.
            // - (200, *) and (205, *) don't conflict because (205, *) is more specific.
            bool HaveWildcardRangeConflict() =>
                     range != StatusCodeRange.All && other != StatusCodeRange.All
                && ((range is (null, _) && other is (_, null) && range.To >= other.From)
                || ( range is (_, null) && other is (null, _) && other.To >= range.From));

        }

        /// <summary>
        ///     Returns a value indicating whether this status code range can be considered
        ///     more specific than the <paramref name="other"/> specified one.
        ///     Specific means that the range defines a "tighter" area of status codes.
        ///     For example, <c>(200, 250)</c> is more specific than <c>(200, 300)</c>.
        ///     
        ///     If the two ranges are not comparable, this returns <see langword="false"/>.
        /// </summary>
        /// <param name="range">This status code range.</param>
        /// <param name="other">The status code range to be compared with this one.</param>
        /// <returns>
        ///     <see langword="true"/> if this range is more specific than the other one;
        ///     <see langword="false"/> if not.
        /// </returns>
        public static bool IsMoreSpecificThan(this StatusCodeRange range, StatusCodeRange other)
        {
            // Short circuit which may save a lot of comparisons. Also required for certain edge cases.
            if (range == other)
            {
                return false;
            }

            return IsSingleAndOtherNot()
                || IsStandardAndOtherNot()
                || IsOneSidedWildcardAndOtherNot()
                || WinsAsMoreSpecificStandardRange()
                || WinsAsMoreSpecificLeftSidedWildcard()
                || WinsAsMoreSpecificRightSidedWildcard();

            bool IsSingleAndOtherNot() =>
                range.IsSingleStatusCode && !other.IsSingleStatusCode;

            bool IsStandardAndOtherNot() =>
                range.IsStandardRange() && other.HasWildcardComponent;

            bool IsOneSidedWildcardAndOtherNot() =>
                range.IsOneSidedWildcard() && other == StatusCodeRange.All;

            bool WinsAsMoreSpecificStandardRange() =>
                range.IsStandardRange() && other.IsStandardRange() && other.IsInRange(range);

            bool WinsAsMoreSpecificLeftSidedWildcard() =>
                range.IsLeftSidedWildcard() && other.IsLeftSidedWildcard() && range.To < other.To;

            bool WinsAsMoreSpecificRightSidedWildcard() =>
                range.IsRightSidedWildcard() && other.IsRightSidedWildcard() && range.From > other.From;
        }

        private static bool IsStandardRange(in this StatusCodeRange range) =>
            !range.IsSingleStatusCode && !range.HasWildcardComponent;

        private static bool IsLeftSidedWildcard(in this StatusCodeRange range) =>
            range.From == Wildcard && range.To != Wildcard;
        
        private static bool IsRightSidedWildcard(in this StatusCodeRange range) =>
            range.To == Wildcard && range.From != Wildcard;

        private static bool IsOneSidedWildcard(in this StatusCodeRange range) =>
            range.From == Wildcard ^ range.To == Wildcard;

    }

}
