namespace ReqRest.Builders.Tests.HttpProtocolVersionBuilderExtensions
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class SetVersionTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void SetVersion_Sets_Version()
        {
            var expected = new Version(13, 7);
            Builder.SetVersion(expected);
            Builder.HttpRequestMessage.Version.Should().BeSameAs(expected);
        }

    }

}
