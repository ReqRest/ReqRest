namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetContentTests : HttpRequestBuilderTestBase
    {

        private static readonly byte[] s_bytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static readonly string s_string = "Hello World";

        [Theory]
        [MemberData(nameof(SetContentData))]
        public void SetContent_Sets_Content(HttpContent content)
        {
            Builder.SetContent(content);
            Builder.HttpRequestMessage.Content.Should().BeSameAs(content);
        }

        public static TheoryData<HttpContent> SetContentData => new TheoryData<HttpContent>()
            { null, new ByteArrayContent(new byte[0]) };

        [Fact]
        public void SetContent_String_Throws_ArgumentNullException_For_Content()
        {
            Action testCode = () => Builder.SetContent((string)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetContent_String_Creates_StringContent()
        {
            Builder.SetContent(s_string);
            Builder.HttpRequestMessage.Content.Should().BeOfType<StringContent>();
        }

        [Theory]
        [InlineData(null, "utf-8")] // UTF8 by default
        [InlineData("utf-16", "utf-16")]
        [InlineData("ascii", "ascii")]
        public async Task SetContent_String_Uses_Encoding_For_Serializing_String(string encodingStr, string expectedEncodingStr)
        {
            var encoding = encodingStr is null ? null : Encoding.GetEncoding(encodingStr);
            var expectedEncoding = Encoding.GetEncoding(expectedEncodingStr);
            var expectedBytes = expectedEncoding.GetBytes(s_string);

            Builder.SetContent(s_string, encoding);
            var bytes = await Builder.HttpRequestMessage.Content.ReadAsByteArrayAsync();

            bytes.Should().Equal(expectedBytes);
        }

        [Fact]
        public void SetContent_String_Sets_MediaType()
        {
            var mediaType = "application/test";
            Builder.SetContent(s_string, mediaType: mediaType);

            var content = (StringContent)Builder.HttpRequestMessage.Content;
            content.Headers.ContentType.MediaType.Should().Be(mediaType);
        }

        [Fact]
        public void SetContent_Bytes_Throws_ArgumentNullException_For_Content()
        {
            Action testCode = () => Builder.SetContent((byte[])null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetContent_Bytes_Creates_ByteArrayContent()
        {
            Builder.SetContent(s_bytes);
            Builder.HttpRequestMessage.Content.Should().BeOfType<ByteArrayContent>();
        }

        [Fact]
        public async Task SetContent_Bytes_Uses_Specified_Bytes()
        {
            Builder.SetContent(s_bytes);
            var bytes = await Builder.HttpRequestMessage.Content.ReadAsByteArrayAsync();

            bytes.Should().Equal(s_bytes);
        }

        [Fact]
        public async Task SetContent_Bytes_Correctly_Uses_Offset_And_Count()
        {
            Builder.SetContent(s_bytes, 1, 8);
            var bytes = await Builder.HttpRequestMessage.Content.ReadAsByteArrayAsync();

            bytes.Should().Equal(s_bytes.Skip(1).Take(8));
        }

    }

}
