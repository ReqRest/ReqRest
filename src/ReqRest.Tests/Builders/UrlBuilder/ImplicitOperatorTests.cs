namespace ReqRest.Tests.Builders.UrlBuilder
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class ImplicitOperatorTests
    {

        [Fact]
        public void Can_Convert_To_Uri()
        {
            var builder = new UrlBuilder("https://test.com?foo=bar");
            var explicitUri = builder.Uri;
            var implicitUri = (Uri)builder;
            explicitUri.Should().BeEquivalentTo(implicitUri);
        }

        [Fact]
        public void Throws_UriFormatException_For_Invalid_Uri()
        {
            var builder = new UrlBuilder()
            {
                Host = "foo\\\\",
            };

            Action testCode = () => _ = (Uri)builder;
            testCode.Should().Throw<UriFormatException>();
        }
        
    }

}
