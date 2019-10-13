namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetStringContentTests : HttpRequestBuilderTestBase
    {

        private const string ContentString = "Hello World";

        [Fact]
        public void Throws_ArgumentNullException_For_Content()
        {
            Action testCode = () => Builder.SetStringContent((string)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Creates_StringContent()
        {
            Builder.SetStringContent(ContentString);
            Builder.HttpRequestMessage.Content.Should().BeOfType<StringContent>();
        }

        [Theory]
        [InlineData(null, "utf-8")] // UTF8 by default
        [InlineData("utf-16", "utf-16")]
        [InlineData("ascii", "ascii")]
        public async Task Uses_Encoding_For_Serializing_String(string encodingStr, string expectedEncodingStr)
        {
            var encoding = encodingStr is null ? null : Encoding.GetEncoding(encodingStr);
            var expectedEncoding = Encoding.GetEncoding(expectedEncodingStr);
            var expectedBytes = expectedEncoding.GetBytes(ContentString);

            Builder.SetStringContent(ContentString, encoding);
            var bytes = await Builder.HttpRequestMessage.Content.ReadAsByteArrayAsync();

            bytes.Should().Equal(expectedBytes);
        }

        [Fact]
        public void Sets_MediaType()
        {
            var mediaType = "application/test";
            Builder.SetStringContent(ContentString, mediaType: mediaType);

            var content = (StringContent)Builder.HttpRequestMessage.Content;
            content.Headers.ContentType.MediaType.Should().Be(mediaType);
        }

    }

}
