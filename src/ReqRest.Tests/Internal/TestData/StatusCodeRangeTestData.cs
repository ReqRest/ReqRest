namespace ReqRest.Tests.Internal.TestData
{
    using ReqRest.Http;
    using Xunit;

    public class StatusCodeRangeTestData
    {

        /// <summary>
        ///     Gets test data with status code ranges which conflict with each other.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> ConflictingRanges => new TheoryData<StatusCodeRange, StatusCodeRange>()
        {
            // Single status codes.
            { 200, 200 },
            { -200, -200 },

            // Standard ranges (positive).
            { (200, 300), (200, 300) }, // Same.
            { (200, 300), (300, 400) }, // Edge overlap.
            { (200, 300), (100, 200) },
            { (200, 300), (250, 400) }, // Strong overlap.
            { (200, 300), (100, 250) },
                
            // Standard ranges (negative).
            { (-300, -200), (-300, -200) }, // Same.
            { (-300, -200), (-400, -300) }, // Edge overlap.
            { (-300, -200), (-200, -100) },
            { (-300, -200), (-400, -250) }, // Strong overlap.
            { (-300, -200), (-250, -100) },

            // Standard ranges (positive and negative).
            { (-300, 300), (200, 400) },
            { (-300, 300), (-400, -200) },

            // Left-sided wildcards (positive).
            { (null, 200), (null, 200) }, // Same.
            { (null, 200), (200, null) }, // Edge overlap.
            { (null, 200), (100, null) }, // Strong overlap.

            // Left-sided wildcards (negative).
            { (null, -200), (null, -200) }, // Same.
            { (null, -200), (-200, null) }, // Edge overlap.
            { (null, -100), (-200, null) }, // Strong overlap.

            // Left-sided wildcards (positive and negative).
            { (null, 200), (-200, null) },

            // Right-sided wildcards (positive).
            { (200, null), (200, null) }, // Same.
            { (200, null), (null, 200) }, // Edge overlap.
            { (200, null), (null, 300) }, // Strong overlap.
                
            // Right-sided wildcards (negative).
            { (-200, null), (-200, null) }, // Same.
            { (-200, null), (null, -200) }, // Edge overlap.
            { (-300, null), (null, -200) }, // Strong overlap.
                
            // Right-sided wildcards (positive and negative).
            { (-200, null), (null, 200) },

            // Wildcard.
            { null, null }, // Same. 
        };

        /// <summary>
        ///     Gets test data with status code ranges which don't conflict with each other.
        ///     This includes a lot of edge cases which should be passed.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> NonConflictingRanges => new TheoryData<StatusCodeRange, StatusCodeRange>()
        {
            // Single status codes.
            { 200, 300 },

            // Standard ranges (positive).
            { (200, 300), 100 },
            { (200, 300), 200 },
            { (200, 300), 250 },
            { (200, 300), (201, 300) }, // One range inside of the other.
            { (200, 300), (200, 299) },
            { (200, 300), (201, 299) },

            // Standard ranges (negative).
            { (-300, -200), -100 },
            { (-300, -200), -200 },
            { (-300, -200), -250 },
            { (-300, -200), (-300, -201) }, // One range inside of the other.
            { (-300, -200), (-299, -200) },
            { (-300, -200), (-299, -201) },

            // Standard ranges (positive and negative).
            { (200, 300), -100 },
            { (-300, -200), 100 },
            { (200, 300), (-300, -200) },

            // Left-sided wildcards (positive).
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
                
            // Left-sided wildcards (negative).
            { (null, -200), -100 },
            { (null, -200), -200 },
            { (null, -200), -100 },
            { (null, -200), (-200, -100) },  // Standard ranges.
            { (null, -200), (-300, -200) },
            { (null, -200), (-300, -100) },
            { (null, -200), (null, -100) }, // Other left-sided wildcards.
            { (null, -200), (null, -300) },
            { (null, -200), (-199, null) }, // Right-sided wildcards without overlap.
            { (null, -200), (-100, null) },

            // Left-sided wildcards (positive and negative).
            { (null, -200), 100 },
            { (null, -200), (null, 100) },
            { (null, 200), -100 },
            { (null, 200), (null, -100) },

            // Right-sided wildcards (positive).
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
                
            // Right-sided wildcards (negative).
            { (-200, null), -100 },
            { (-200, null), -200 },
            { (-200, null), -300 },
            { (-200, null), (-200, -100) },  // Standard ranges.
            { (-200, null), (-300, -200) },
            { (-200, null), (-300, -200) },
            { (-200, null), (-100, null) }, // Other right-sided wildcards.
            { (-200, null), (-300, null) },
            { (-200, null), (null, -201) }, // Left-sided wildcards without overlap.
            { (-200, null), (null, -300) },
                
            // Right -sided wildcards (positive and negative).
            { (-200, null), 100 },
            { (-200, null), (100, null) },
            { (200, null), -100 },
            { (200, null), (-100, null) },

            // Wildcard (positive).
            { null, 100 },
            { null, (200, 300) },
            { null, (null, 200) },
            { null, (200, null) },

            // Wildcard (negative).
            { null, -100 },
            { null, (-300, -200) },
            { null, (null, -200) },
            { null, (-200, null) },
        };

        /// <summary>
        ///     Gets test data of two status code ranges, where the first one is less specific than
        ///     the second one.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> LessSpecificThanData => new TheoryData<StatusCodeRange, StatusCodeRange>()
        {
            // Wildcard.
            { null, (200, 200) },
            { null, (-200, -200) },

            { null, (200, 300) },
            { null, (-300, 200) },

            { null, (200, null) },
            { null, (-200, null) },

            { null, (null, 200) },
            { null, (null, -200) },

            // Left-sided wildcard.
            { (200, null), (200, 200) },
            { (-200, null), (-200, -200) },

            { (200, null), (200, 300) },
            { (-200, null), (-300, -200) },

            { (200, null), (201, null) },   // (201 - *) covers less than (200 - *).
            { (-200, null), (-199, null) }, // (-199 - *) covers less than (200 - *).

            // Right-sided wildcard.
            { (null, 200), (200, 200) },
            { (null, -200), (-200, -200) },

            { (null, 200), (200, 300) },
            { (null, -200), (-300, -200) },

            { (null, 200), (null, 100) }, // (*-100) covers less than (*-200)
            { (null, -200), (null, -300) }, // (*-300) covers less than (*-200)
                
            // Standard range.
            { (200, 300), (200, 200) },
            { (-300, -200), (-200, -200) },

            { (200, 300), (201, 300) }, // InRange.
            { (-300, -200), (-299, -200) }, // InRange.

            { (200, 300), (200, 299) },
            { (-300, -200), (-300, -201) },

            { (200, 300), (201, 299) },
            { (-300, -200), (-299, -201) },
        };

        /// <summary>
        ///     Gets test data of two status code ranges, where the two are considered to be
        ///     equally specific.
        /// </summary>
        public static TheoryData<StatusCodeRange, StatusCodeRange> EquallySpecificData => new TheoryData<StatusCodeRange, StatusCodeRange>()
        {
            // Single status codes.
            { (200, 200), (201, 201) },

            // Standard range.
            { (200, 300), (400, 500) },
            { (-300, -200), (-500, -400) },
            { (-300, -200), (400, 500) },

            // Left-sided wildcard.
            { (null, 200), (300, null) },
            { (null, -200), (-300, null) },
            { (null, -200), (300, null) },

            // Right-sided wildcard.
            { (200, null), (null, 100) },
            { (-200, null), (null, -100) },
            { (-200, null), (null, 100) },
        };

    }

}
