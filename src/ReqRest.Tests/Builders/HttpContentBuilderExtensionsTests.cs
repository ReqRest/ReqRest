namespace ReqRest.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.TestBases;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using Xunit;
    using System.Net.Http;
    using ReqRest.Tests.Sdk.Data;
    using System.Text;

    public class HttpContentBuilderExtensionsTests
    {

        public class SetContentTests : BuilderTestBase
        {

            public static TheoryData<HttpContent?> SetContentData => new TheoryData<HttpContent?>()
            {
                null,
                new ByteArrayContent(Array.Empty<byte>())
            };

            [Theory]
            [MemberData(nameof(SetContentData))]
            public void SetContent_Sets_Content(HttpContent content)
            {
                Service.SetContent(content);
                Assert.Same(content, Service.Content);
            }

        }

        public class SetByteArrayContentTests : BuilderTestBase
        {

            private static readonly byte[] ContentBytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            [Fact]
            public void Creates_ByteArrayContent()
            {
                Service.SetByteArrayContent(ContentBytes);
                Assert.IsType<ByteArrayContent>(Service.Content);
            }

            [Fact]
            public async Task Uses_Specified_Bytes()
            {
                Service.SetByteArrayContent(ContentBytes);
                var bytes = await Service.Content!.ReadAsByteArrayAsync();
                Assert.Equal(ContentBytes, bytes);
            }

            [Fact]
            public async Task Uses_Offset_And_Count()
            {
                Service.SetByteArrayContent(ContentBytes, 1, 8);
                var bytes = await Service.Content!.ReadAsByteArrayAsync();
                Assert.Equal(ContentBytes.Skip(1).Take(8), bytes);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(IHttpContentBuilder builder, byte[] bytes)
            {
                Assert.Throws<ArgumentNullException>(() => HttpContentBuilderExtensions.SetByteArrayContent(builder, bytes));
            }

        }

        public class SetStringContentTests : BuilderTestBase
        {

            private const string ContentString = "Hello World";

            [Fact]
            public void Creates_StringContent()
            {
                Service.SetStringContent(ContentString);
                Assert.IsType<StringContent>(Service.Content);
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

                Service.SetStringContent(ContentString, encoding);
                var bytes = await Service.Content!.ReadAsByteArrayAsync();

                Assert.Equal(expectedBytes, bytes);
            }

            [Fact]
            public void Sets_MediaType()
            {
                var mediaType = "application/test";
                Service.SetStringContent(ContentString, mediaType: mediaType);

                var content = (StringContent)Service.Content!;
                Assert.Equal(mediaType, content.Headers.ContentType.MediaType);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull, Null, Null)]
            public void Throws_ArgumentNullException(IHttpContentBuilder builder, string content = "", Encoding? encoding = null, string? mediaType = null)
            {
                Assert.Throws<ArgumentNullException>(() => HttpContentBuilderExtensions.SetStringContent(builder, content, encoding, mediaType));
            }

        }

        public class SetFormUrlEncodedContentTests : BuilderTestBase
        {

            [Fact]
            public void Sets_Content_To_FormUrlEncodedContent()
            {
                Service.SetFormUrlEncodedContent(("Key", "Value"));
                Assert.IsType<FormUrlEncodedContent>(Service.Content);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void TupleArray_Throws_ArgumentNullException(IHttpContentBuilder builder, params (string, string)[] content)
            {
                Assert.Throws<ArgumentNullException>(() => HttpContentBuilderExtensions.SetFormUrlEncodedContent(builder, content));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void TupleEnumerable_Throws_ArgumentNullException(IHttpContentBuilder builder, IEnumerable<(string, string)> content)
            {
                Assert.Throws<ArgumentNullException>(() => HttpContentBuilderExtensions.SetFormUrlEncodedContent(builder, content));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void KeyValuePairArray_Throws_ArgumentNullException(IHttpContentBuilder builder, params KeyValuePair<string, string>[] content)
            {
                Assert.Throws<ArgumentNullException>(() => HttpContentBuilderExtensions.SetFormUrlEncodedContent(builder, content));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void KeyValuePairEnumerable_Throws_ArgumentNullException(IHttpContentBuilder builder, IEnumerable<KeyValuePair<string, string>> content)
            {
                Assert.Throws<ArgumentNullException>(() => HttpContentBuilderExtensions.SetFormUrlEncodedContent(builder, content));
            }

            // Other tests are hard, because there is no easy way to get to the data once converted to an encoded content.
            // TODO: Add more tests here.

        }

    }

}
