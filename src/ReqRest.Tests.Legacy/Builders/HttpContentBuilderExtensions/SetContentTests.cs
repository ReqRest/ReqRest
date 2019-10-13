namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetContentTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [MemberData(nameof(SetContentData))]
        public void SetContent_Sets_Content(HttpContent content)
        {
            Builder.SetContent(content);
            Builder.HttpRequestMessage.Content.Should().BeSameAs(content);
        }

        public static TheoryData<HttpContent> SetContentData => new TheoryData<HttpContent>()
            { null, new ByteArrayContent(new byte[0]) };

    }

}
