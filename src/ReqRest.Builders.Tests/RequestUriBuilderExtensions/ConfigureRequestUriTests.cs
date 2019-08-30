namespace ReqRest.Builders.Tests.RequestUriBuilderExtensions
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public abstract class ConfigureRequestUriTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void Creates_Url_Builder_With_Same_Uri_If_Present_Action()
        {
            var uri = new Uri("http://test.de/123");
            Builder
                .SetRequestUri(uri)
                .ConfigureRequestUri(builder =>
                {
                    builder.Uri.Should().Be(Builder.HttpRequestMessage.RequestUri);
                });
        }
        
        [Fact]
        public void Creates_Url_Builder_With_Same_Uri_If_Present_Func()
        {
            var uri = new Uri("http://test.de/123");
            Builder
                .SetRequestUri(uri)
                .ConfigureRequestUri(builder =>
                {
                    builder.Uri.Should().Be(Builder.HttpRequestMessage.RequestUri);
                    return builder;
                });
        }
        
        [Fact]
        public void Creates_Default_Url_Builder_Without_Request_Uri_Action()
        {
            var expectedUri = new Builders.UrlBuilder().Uri;
            
            Builder.ConfigureRequestUri(builder =>
            {
                builder.Uri.Should().Be(expectedUri);
            });
        }

        [Fact]
        public void Creates_Default_Url_Builder_Without_Request_Uri_Func()
        {
            var expectedUri = new Builders.UrlBuilder().Uri;
            
            Builder.ConfigureRequestUri(builder =>
            {
                builder.Uri.Should().Be(expectedUri);
                return builder;
            });
        }

        [Fact]
        public void Calls_Configure_Action()
        {
            bool wasCalled = false;
            Builder.ConfigureRequestUri(_ => wasCalled = true);
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Calls_Configure_Func()
        {
            bool wasCalled = false;
            Builder.ConfigureRequestUri(builder =>
            {
                wasCalled = true;
                return builder;
            });
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Configure_Action()
        {
            Action testCode = () => Builder.ConfigureRequestUri((Action<UrlBuilder>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Configure_Func()
        {
            Action testCode = () => Builder.ConfigureRequestUri((Func<UrlBuilder, Uri>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
