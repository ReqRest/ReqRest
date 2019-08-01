namespace ReqRest.Builders.Tests.UrlBuilder
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class ConstructorTests
    {

        public static TheoryData<string> Urls = new TheoryData<string>()
        {
            "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top",
        };
        
        [Theory]
        [MemberData(nameof(Urls))]
        public void Initializes_From_String(string url)
        {
            var builder = new UrlBuilder(url);
            VerifyProperties(builder, new Uri(url));
        }

        [Theory]
        [MemberData(nameof(Urls))]
        public void Initializes_From_Uri(string url)
        {
            var uri = new Uri(url);
            var builder = new UrlBuilder(url);
            VerifyProperties(builder, uri);
        }

        [Fact]
        public void Initializes_With_Special_Values()
        {
            var builder = new UrlBuilder(
                scheme: "https",
                host: "my-host",
                port: 1307,
                path: "test",
                extraValue: "#fragment"
            );

            builder.Scheme.Should().BeEquivalentTo("https");
            builder.Host.Should().BeEquivalentTo("my-host");
            builder.Port.Should().Be(1307);
            builder.Path.Should().BeEquivalentTo("test");
            builder.Fragment.Should().BeEquivalentTo("#fragment");
        }
        
        private static void VerifyProperties(UrlBuilder builder, Uri url)
        {
            // The values should be equivalent to the base class.
            var reference = new UriBuilder(url);
            builder.Scheme.Should().BeEquivalentTo(reference.Scheme);
            builder.Host.Should().BeEquivalentTo(reference.Host);
            builder.Path.Should().BeEquivalentTo(reference.Path);
            builder.Query.Should().BeEquivalentTo(reference.Query);
            builder.Fragment.Should().BeEquivalentTo(reference.Fragment);
            builder.UserName.Should().BeEquivalentTo(reference.UserName);
            builder.Password.Should().BeEquivalentTo(reference.Password);
        }
        
    }

}
