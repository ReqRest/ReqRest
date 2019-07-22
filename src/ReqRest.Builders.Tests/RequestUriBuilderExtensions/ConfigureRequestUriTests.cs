namespace ReqRest.Builders.Tests.RequestUriBuilderExtensions
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class ConfigureRequestUriTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void Creates_Url_Builder_With_Same_Uri_If_Present()
        {
            var uri = new Uri("http://test.de/123");
            Builder.SetRequestUri(uri);

            Builder.ConfigureRequestUri(builder =>
            {
                builder.Uri.Should().Be(Builder.HttpRequestMessage.RequestUri);
            });
        }
        
        [Fact]
        public void Creates_Default_Url_Builder_Without_Request_Uri()
        {
            var expectedUri = new Builders.UrlBuilder().Uri;

            Builder.ConfigureRequestUri(builder =>
            {
                builder.Uri.Should().Be(expectedUri);
            });
        }

        [Fact]
        public void Calls_Specified_Action()
        {
            bool wasCalled = false;
            Builder.ConfigureRequestUri(_ => wasCalled = true);
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.ConfigureRequestUri(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
