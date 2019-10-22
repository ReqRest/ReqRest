namespace ReqRest.Tests.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using ReqRest.Tests.Internal.TestData;
    using ReqRest.Http;
    using ReqRest.Internal;

    public class ResponseTypeDescriptorCollectionTests
    {

        public class ThrowsForConflictsTests
        {

            public static IEnumerable<object[]> ConflictData { get; } =
                StatusCodeRangeTestData.ConflictingRanges
                    .Select(parameters =>
                        parameters.Select(range =>
                            (object)new ResponseTypeDescriptor(
                                typeof(object), new StatusCodeRange[] { (StatusCodeRange)range }, () => null!)
                        )
                        .ToArray());

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Add_Throws_For_Conflicts(ResponseTypeDescriptor a, ResponseTypeDescriptor b)
            {
                var collection = new ResponseTypeDescriptorCollection() { a };
                Assert.Throws<InvalidOperationException>(() => collection.Add(b));
            }

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Insert_Throws_For_Conflicts(ResponseTypeDescriptor a, ResponseTypeDescriptor b)
            {
                var collection = new ResponseTypeDescriptorCollection() { a };
                Assert.Throws<InvalidOperationException>(() => collection.Insert(0, b));
            }

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Indexer_Throws_For_Conflicts(ResponseTypeDescriptor a, ResponseTypeDescriptor b)
            {
                var collection = new ResponseTypeDescriptorCollection()
                {
                    a,
                    new ResponseTypeDescriptor(typeof(object), new StatusCodeRange[] { 0 }, () => null!),
                };
                Assert.Throws<InvalidOperationException>(() => collection[1] = b);
            }

            [Theory]
            [MemberData(nameof(ConflictData))]
            public void Indexer_Doesnt_Throw_If_Replacing_Conflicting(ResponseTypeDescriptor a, ResponseTypeDescriptor b)
            {
                var collection = new ResponseTypeDescriptorCollection() { a };
                collection[0] = b; // Should not throw.
            }

        }

    }

}
