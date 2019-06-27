namespace ReqRest.Client.Tests.ResponseTypeInfoCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using ReqRest;
    using Xunit;

    public class ThrowsForConflictsTests
    {

        public static IEnumerable<object[]> ConflictData { get; } =
            StatusCodeRangeData.ConflictingRanges
                .Select(parameters =>
                    parameters.Select(range => 
                        (object)new ResponseTypeInfo(
                            typeof(object), new StatusCodeRange[] { (StatusCodeRange)range }, () => null)
                    )
                    .ToArray());

        [Theory]
        [MemberData(nameof(ConflictData))]
        public void Add_Throws_For_Conflicts(ResponseTypeInfo a, ResponseTypeInfo b)
        {
            var collection = new ResponseTypeInfoCollection() { a };
            Action testCode = () => collection.Add(b);
            testCode.Should().Throw<InvalidOperationException>();
        }
        
        [Theory]
        [MemberData(nameof(ConflictData))]
        public void Insert_Throws_For_Conflicts(ResponseTypeInfo a, ResponseTypeInfo b)
        {
            var collection = new ResponseTypeInfoCollection() { a };
            Action testCode = () => collection.Insert(0, b);
            testCode.Should().Throw<InvalidOperationException>();
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
            Action testCode = () => collection[1] = b;
            testCode.Should().Throw<InvalidOperationException>();
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
