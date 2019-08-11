namespace ReqRest.Tests
{
    using ReqRest.Http;
    using Xunit;

    /// <summary>
    ///     Contains test data for <see cref="StatusCodeRange"/> objects which conflict with each other.
    /// </summary>
    public static class StatusCodeRangeData
    {

        /// <summary>
        ///     Gets test data with status code ranges which conflict with each other.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> ConflictingRanges => 
            new TheoryData<StatusCodeRange, StatusCodeRange>()
            {
                // Single status codes.
                { 200, 200 }, // Same.

                // Standard ranges.
                { (200, 300), (200, 300) }, // Same.
                { (200, 300), (300, 400) }, // Edge overlap.
                { (200, 300), (100, 200) },
                { (200, 300), (250, 400) }, // Strong overlap.
                { (200, 300), (100, 250) },

                // Left-sided wildcards.
                { (null, 200), (null, 200) }, // Same.
                { (null, 200), (200, null) }, // Edge overlap.
                { (null, 200), (100, null) }, // Strong overlap.

                // Right-sided wildcards.
                { (200, null), (200, null) }, // Same.
                { (200, null), (null, 200) }, // Edge overlap.
                { (200, null), (null, 300) }, // Strong overlap.

                // Wildcard.
                { null, null }, // Same. 
            };

        /// <summary>
        ///     Gets test data with status code ranges which don't conflict with each other.
        ///     This includes a lot of edge cases which should be passed.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> NonConflictingRanges =>
            new TheoryData<StatusCodeRange, StatusCodeRange>()
            {
                // Single status codes.
                { 200, 300 },

                // Standard ranges.
                { (200, 300), 100 },
                { (200, 300), 200 },
                { (200, 300), 250 },
                { (200, 300), (201, 300) }, // One range inside of the other.
                { (200, 300), (200, 299) },
                { (200, 300), (201, 299) },

                // Left-sided wildcards.
                { (null, 200), 100 },
                { (null, 200), 200 },
                { (null, 200), 300 },
                { (null, 200), (100, 200) },  // Standard ranges.
                { (null, 200), (200, 300) },
                { (null, 200), (100, 300) },
                { (null, 200), (null, 100) }, // Other left-sided wildcards.
                { (null, 200), (null, 300) },
                { (null, 200), (201, null) }, // Right-sided wildcards without overlap.
                { (null, 200), (300, null) },

                // Right-sided wildcards.
                { (200, null), 100 },
                { (200, null), 200 },
                { (200, null), 300 },
                { (200, null), (100, 200) },  // Standard ranges.
                { (200, null), (200, 300) },
                { (200, null), (100, 300) },
                { (200, null), (100, null) }, // Other right-sided wildcards.
                { (200, null), (300, null) },
                { (200, null), (null, 199) }, // Left-sided wildcards without overlap.
                { (200, null), (null, 100) },

                // Wildcard.
                { null, 100 },
                { null, (200, 300) },
                { null, (null, 200) },
                { null, (200, null) },
            };

        /// <summary>
        ///     Gets test data of two status code ranges, where the first one is less specific than
        ///     the second one.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> LessSpecificThanData =>
            new TheoryData<StatusCodeRange, StatusCodeRange>()
            {
                // Wildcard.
                { null, (200, 200) },
                { null, (200, 300) },
                { null, (200, null) },
                { null, (null, 200) },

                // Left-sided wildcard.
                { (200, null), (200, 200) },
                { (200, null), (200, 300) },
                { (200, null), (201, null) }, // (201-*) covers less than (200-*).

                // Right-sided wildcard.
                { (null, 200), (200, 200) },
                { (null, 200), (200, 300) },
                { (null, 200), (null, 100) }, // (*-100) covers less than (*-200)
                
                // Standard range.
                { (200, 300), (200, 200) },
                { (200, 300), (201, 300) }, // InRange.
                { (200, 300), (200, 299) },
                { (200, 300), (201, 299) },
            };

        /// <summary>
        ///     Gets test data of two status code ranges, where the two are considered to be
        ///     equally specific.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> EquallySpecificData =>
            new TheoryData<StatusCodeRange, StatusCodeRange>()
            {
                // Single status codes.
                { (200, 200), (201, 201) },

                // Standard range.
                { (200, 300), (400, 500) },

                // Left-sided wildcard.
                { (null, 200), (300, null) },

                // Right-sided wildcard.
                { (200, null), (null, 100) },
            };

    }

}
