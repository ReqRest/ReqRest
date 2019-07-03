namespace ReqRest.Builders.Tests.UriBuilderExtensions
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class AppendPathTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [MemberData(nameof(UriBuilderData.AppendPathData), MemberType = typeof(UriBuilderData))]
        public void Appends_Path_To_End(string initialPath, IEnumerable<string> toAppend, string expected)
        {
            Builder.Path = initialPath;

            foreach (var segment in toAppend)
            {
                Builder.AppendPath(segment);
            }

            Builder.Path.Should().Be(expected);
        }

    }

}
