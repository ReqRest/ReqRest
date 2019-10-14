namespace ReqRest.Tests.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using ReqRest.Tests.Internal.TestData;
    using ReqRest.Http;
    using ReqRest.Internal;

    public class ResponseTypeInfoCollectionTests
    {

        public class ThrowsForConflictsTests
        {

            public static IEnumerable<object[]> ConflictData { get; } =
                StatusCodeRangeTestData.ConflictingRanges
                    .Select(parameters =>
                        parameters.Select(range =>
                            (object)new ResponseTypeInfo(
                                typeof(object), new StatusCodeRange[] { (StatusCodeRange)range }, () => null!)
                        )
                        .ToArray());

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Add_Throws_For_Conflicts(ResponseTypeInfo a, ResponseTypeInfo b)
            {
                var collection = new ResponseTypeInfoCollection() { a };
                Assert.Throws<InvalidOperationException>(() => collection.Add(b));
            }

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Insert_Throws_For_Conflicts(ResponseTypeInfo a, ResponseTypeInfo b)
            {
                var collection = new ResponseTypeInfoCollection() { a };
                Assert.Throws<InvalidOperationException>(() => collection.Insert(0, b));
            }

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Indexer_Throws_For_Conflicts(ResponseTypeInfo a, ResponseTypeInfo b)
            {
                var collection = new ResponseTypeInfoCollection()
                {
                    a,
                    new ResponseTypeInfo(typeof(object), new StatusCodeRange[] { 0 }, () => null),
                };
                Assert.Throws<InvalidOperationException>(() => collection[1] = b);
            }

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Indexer_Doesnt_Throw_If_Replacing_Conflicting(ResponseTypeInfo a, ResponseTypeInfo b)
            {
                var collection = new ResponseTypeInfoCollection() { a };
                collection[0] = b; // Should not throw.
            }

        }

    }

}
