namespace ReqRest.Tests.Http.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class PredefinedRangesTests
    {

        [Theory]
        [MemberData(nameof(PredefinedValuesData))]
        public void Have_Expected_Values(StatusCodeRange range, int? expectedFrom, int? expectedTo)
        {
            range.From.Should().Be(expectedFrom);
            range.To.Should().Be(expectedTo);
        }

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

    }

}
