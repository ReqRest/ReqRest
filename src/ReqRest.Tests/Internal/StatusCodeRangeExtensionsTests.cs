namespace ReqRest.Tests.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Xunit;
    using ReqRest.Http;
    using ReqRest.Tests.Internal.TestData;
    using ReqRest.Internal;

    public class StatusCodeRangeExtensionsTests
    {

        public class ConflictsWithTests
        {

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.ConflictingRanges), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_True_For_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
            {
                Assert.True(x.ConflictsWith(y));
                Assert.True(y.ConflictsWith(x));
            }

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.NonConflictingRanges), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_False_For_Non_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
            {
                Assert.False(x.ConflictsWith(y));
                Assert.False(y.ConflictsWith(x));
            }

        }

        public class IsMoreSpecificThanTests
        {

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_True_For_More_Specific_Range(StatusCodeRange x, StatusCodeRange y)
            {
                // By just reversing the less specific data, we should get a valid test result.
                Assert.True(y.IsMoreSpecificThan(x));
            }

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_False_For_Less_Specific_Range(StatusCodeRange x, StatusCodeRange y)
            {
                Assert.False(x.IsMoreSpecificThan(y));
            }

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.EquallySpecificData), MemberType = typeof(StatusCodeRangeTestData))]
            [MemberData(nameof(StatusCodeRangeTestData.ConflictingRanges), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_False_For_Equal_And_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
            {
                Assert.False(x.IsMoreSpecificThan(y));
            }

        }

    }

}
