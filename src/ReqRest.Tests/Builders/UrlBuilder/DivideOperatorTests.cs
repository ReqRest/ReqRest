namespace ReqRest.Tests.Builders.UrlBuilder
{
    using System.Collections.Generic;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class DivideOperatorTests
    {

        [Theory]
        [MemberData(nameof(UriBuilderData.AppendPathData), MemberType = typeof(UriBuilderData))]
        public void Appends_Path_To_End(string initialPath, IEnumerable<string> toAppend, string expected)
        {
            var builder = new UrlBuilder(path: initialPath);

            foreach (var segment in toAppend)
            {
                builder = builder / segment;
            }
            
            builder.Path.Should().Be(expected);
        }

    }

}
