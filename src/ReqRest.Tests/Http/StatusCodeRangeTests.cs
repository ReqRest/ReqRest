namespace ReqRest.Tests.Http
{
    using System;
    using ReqRest.Http;
    using ReqRest.Tests.Sdk.TestRecipes;
    using Xunit;

    public class StatusCodeRangeTests
    {

        public class PredefinedRangesTests
        {

            public static TheoryData<StatusCodeRange, int?, int?> PredefinedValuesData { get; } =
                new TheoryData<StatusCodeRange, int?, int?>()
                {
                    { StatusCodeRange.All, null, null },
                    { StatusCodeRange.Informational, 100, 199 },
                    { StatusCodeRange.Success, 200, 299 },
                    { StatusCodeRange.Redirection, 300, 399 },
                    { StatusCodeRange.ClientErrors, 400, 499 },
                    { StatusCodeRange.ServerErrors, 500, 599 },
                    { StatusCodeRange.Errors, 400, 599 },
                };

            [Theory]
            [MemberData(nameof(PredefinedValuesData))]
            public void Have_Expected_Values(StatusCodeRange range, int? expectedFrom, int? expectedTo)
            {
                Assert.Equal(expectedFrom, range.From);
                Assert.Equal(expectedTo, range.To);
            }

        }

        public class ConstructorTests
        {

            [Theory]
            [InlineData(null, null)]
            [InlineData(null, 123)]
            [InlineData(123, null)]
            [InlineData(123, 123)]
            [InlineData(123, 456)]
            public void Initializes_From_And_To(int? from, int? to)
            {
                var range = new StatusCodeRange(from, to);
                Assert.Equal(from, range.From);
                Assert.Equal(to, range.To);
            }

            [Theory]
            [InlineData(null)]
            [InlineData(123)]
            public void Initializes_From_And_To_With_Single_Status_Code(int? statusCode)
            {
                var range = new StatusCodeRange(statusCode);
                Assert.Equal(range.From, statusCode);
                Assert.Equal(range.To, statusCode);
            }

            [Fact]
            public void Throws_ArgumentException_If_From_Is_Less_Than_To()
            {
                Assert.Throws<ArgumentException>(() => new StatusCodeRange(1, 0));
            }

        }

        public class DeconstructTests
        {

            [Theory]
            [InlineData(null, null)]
            [InlineData(null, 200)]
            [InlineData(200, null)]
            [InlineData(200, 200)]
            [InlineData(100, 200)]
            public void Deconstructs_Into_From_And_To(int? from, int? to)
            {
                var range = new StatusCodeRange(from, to);
                range.Deconstruct(out var newFrom, out var newTo);
                Assert.Equal(newFrom, from);
                Assert.Equal(newTo, to);
            }

        }

        public class Equality_StatusCodeRangeTests : EqualityRecipe<StatusCodeRange>
        {

            protected override TheoryData<StatusCodeRange, StatusCodeRange> EqualObjects => new TheoryData<StatusCodeRange, StatusCodeRange>()
            {
                { StatusCodeRange.All, StatusCodeRange.All },
                { (null, 200), (null, 200) },
                { (200, null), (200, null) },
                { (200, 200), (200, 200) },
                { (100, 200), (100, 200) },
            };

            protected override TheoryData<StatusCodeRange, StatusCodeRange> UnequalObjects => new TheoryData<StatusCodeRange, StatusCodeRange>()
            {
                { (200, 200), (100, 200) },
                { (200, 200), (200, null) },
                { (200, 200), (null, 200) },
                { (200, 200), (null, null) },
            };

        }

        public class IsSingleStatusCodeTests
        {

            [Fact]
            public void Returns_True_For_Single_Status_Codes()
            {
                Assert.True(new StatusCodeRange(123, 123).IsSingleStatusCode);
            }

            [Theory]
            [InlineData(123, 456)]
            [InlineData(123, 124)]
            [InlineData(123, null)]
            [InlineData(null, 123)]
            [InlineData(null, null)] // Covers all ranges, even though From == To.
            public void Returns_False_For_Actual_Range(int? from, int? to)
            {
                Assert.False(new StatusCodeRange(from, to).IsSingleStatusCode);
            }

        }

        public class HasWildcardComponentTests
        {

            [Theory]
            [InlineData(null, 123)]
            [InlineData(123, null)]
            [InlineData(null, null)]
            public void Returns_True_For_Wildcard_Ranges(int? from, int? to)
            {
                Assert.True(new StatusCodeRange(from, to).HasWildcardComponent);
            }

            [Fact]
            public void Returns_False_For_Non_Wildcard_Range()
            {
                Assert.False(new StatusCodeRange(123, 456).HasWildcardComponent);
            }

        }

        public class IsInRangeTests
        {

            [Theory]
            [InlineData(null, null, 0)]
            [InlineData(null, null, 50)]
            [InlineData(null, null, int.MaxValue)]
            [InlineData(0, 100, 0)]
            [InlineData(0, 100, 50)]
            [InlineData(0, 100, 100)]
            [InlineData(null, 100, 0)]
            [InlineData(null, 100, 50)]
            [InlineData(null, 100, 100)]
            [InlineData(100, null, 100)]
            [InlineData(100, null, 150)]
            [InlineData(100, null, int.MaxValue)]
            public void Returns_True_For_Single_Status_Codes(int? rangeFrom, int? rangeTo, int statusCode)
            {
                var range = new StatusCodeRange(rangeFrom, rangeTo);
                Assert.True(range.IsInRange(statusCode));
            }

            [Theory]
            [InlineData(0, 100, 101)]
            [InlineData(0, 100, 200)]
            [InlineData(null, 100, 101)]
            [InlineData(null, 100, 200)]
            [InlineData(100, null, 99)]
            [InlineData(100, null, 0)]
            public void Returns_False_For_Single_Status_Codes(int? rangeFrom, int? rangeTo, int statusCode)
            {
                var range = new StatusCodeRange(rangeFrom, rangeTo);
                Assert.False(range.IsInRange(statusCode));
            }

            [Theory]
            [InlineData(0, 100, 0, 100)]
            [InlineData(0, 100, 0, 50)]
            [InlineData(0, 100, 50, 100)]
            [InlineData(0, 100, 0, 0)]
            [InlineData(0, 100, 100, 100)]
            [InlineData(null, 100, null, 100)]
            [InlineData(null, 100, null, 50)]
            [InlineData(null, 100, 0, 100)]
            [InlineData(null, 100, 0, 0)]
            [InlineData(null, 100, 100, 100)]
            [InlineData(100, null, 100, null)]
            [InlineData(100, null, 150, null)]
            [InlineData(100, null, 100, 150)]
            [InlineData(100, null, 100, 100)]
            [InlineData(100, null, 150, 150)]
            [InlineData(null, null, null, null)]
            [InlineData(null, null, 0, null)]
            [InlineData(null, null, null, int.MaxValue)]
            public void Returns_True_For_Other_Range(int? outerFrom, int? outerTo, int? innerFrom, int? innerTo)
            {
                var outer = new StatusCodeRange(outerFrom, outerTo);
                var inner = new StatusCodeRange(innerFrom, innerTo);
                Assert.True(outer.IsInRange(inner));
            }

            [Theory]
            [InlineData(100, 200, 99, 200)]
            [InlineData(100, 200, 100, 201)]
            [InlineData(100, 200, 200, 201)]
            [InlineData(100, 200, 99, 100)]
            [InlineData(100, 200, 300, 400)]
            [InlineData(100, 200, 99, 99)]
            [InlineData(100, 200, 201, 201)]
            [InlineData(null, 100, null, 101)]
            [InlineData(null, 100, 99, 101)]
            [InlineData(null, 100, 200, 200)]
            [InlineData(null, 100, 101, 101)]
            [InlineData(100, null, 99, null)]
            [InlineData(100, null, 99, 101)]
            [InlineData(100, null, 50, 50)]
            [InlineData(100, null, 99, 99)]
            public void Returns_False_For_Other_Range(int? outerFrom, int? outerTo, int? innerFrom, int? innerTo)
            {
                var outer = new StatusCodeRange(outerFrom, outerTo);
                var inner = new StatusCodeRange(innerFrom, innerTo);
                Assert.False(outer.IsInRange(inner));
            }

        }

        public class ToStringTests : ToStringRecipe<StatusCodeRange>
        {

            protected override TheoryData<StatusCodeRange, Func<StatusCodeRange, string?>> Expectations => new TheoryData<StatusCodeRange, Func<StatusCodeRange, string?>>()
            {
                { StatusCodeRange.All, _ => "*" },
                { (200, 200), _ => "200" },
                { (null, 200), _ => "[*, 200]" },
                { (200, null), _ => "[200, *]" },
                { (200, 300), _ => "[200, 300]" },
                { (-300, 200), _ => "[-300, 200]" },
                { (-300, -200), _ => "[-300, -200]" }
            };

        }

        public class ImplicitOperatorTests
        {

            [Theory]
            [InlineData(null, null)]
            [InlineData(null, 123)]
            [InlineData(123, null)]
            [InlineData(123, 123)]
            [InlineData(123, 456)]
            public void Can_Create_From_Tuple(int? from, int? to)
            {
                StatusCodeRange range = (from, to);
                Assert.Equal(from, range.From);
                Assert.Equal(to, range.To);
            }

            [Theory]
            [InlineData(null)]
            [InlineData(123)]
            public void Can_Create_From_Integer(int? statusCode)
            {
                StatusCodeRange range = statusCode;
                Assert.Equal(statusCode, range.From);
                Assert.Equal(statusCode, range.To);
            }

        }

    }

}
