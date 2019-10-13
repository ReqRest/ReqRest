namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetByteArrayContentTests : HttpRequestBuilderTestBase
    {

        private static readonly byte[] ContentBytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        [Fact]
        public void Throws_ArgumentNullException_For_Content()
        {
            Action testCode = () => Builder.SetByteArrayContent((byte[])null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Creates_ByteArrayContent()
        {
            Builder.SetByteArrayContent(ContentBytes);
            Builder.HttpRequestMessage.Content.Should().BeOfType<ByteArrayContent>();
        }

        [Fact]
        public async Task Uses_Specified_Bytes()
        {
            Builder.SetByteArrayContent(ContentBytes);
            var bytes = await Builder.HttpRequestMessage.Content.ReadAsByteArrayAsync();

            bytes.Should().Equal(ContentBytes);
        }

        [Fact]
        public async Task Uses_Offset_And_Count()
        {
            Builder.SetByteArrayContent(ContentBytes, 1, 8);
            var bytes = await Builder.HttpRequestMessage.Content.ReadAsByteArrayAsync();

            bytes.Should().Equal(ContentBytes.Skip(1).Take(8));
        }

    }

}
