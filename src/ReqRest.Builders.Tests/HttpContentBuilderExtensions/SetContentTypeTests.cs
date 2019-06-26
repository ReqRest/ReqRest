namespace ReqRest.Builders.Tests.HttpContentBuilderExtensions
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using FluentAssertions;
    using ReqRest;
    using Xunit;

    public class SetContentTypeTests : HttpRequestBuilderTestBase
    {

        protected MediaTypeHeaderValue ContentType
        {
            get => Builder.HttpRequestMessage.Content.Headers.ContentType;
            set => Builder.HttpRequestMessage.Content.Headers.ContentType = value;
        }

        public SetContentTypeTests()
        {
            Builder.HttpRequestMessage.Content = new StringContent("");
        }

        [Fact]
        public void Single_Parameter_Method_Creates_ContentType_With_Media_Type()
        {
            var mediaType = MediaType.ApplicationJson;
            Builder.SetContentType(mediaType);
            ContentType.MediaType.Should().Be(mediaType);
        }

        [Fact]
        public void Single_Parameter_Method_Creates_ContentType_With_CharSet()
        {
            var charSet = Encoding.ASCII.WebName;
            Builder.SetContentType(MediaType.ApplicationJson, charSet);
            ContentType.CharSet.Should().Be(charSet);
        }

        [Fact]
        public void Single_Parameter_Method_Creates_ContentType_With_Parameters()
        {
            var parameters = new[]
            {
                new NameValueHeaderValue("p1"),
                new NameValueHeaderValue("p2"),
            };

            Builder.SetContentType(MediaType.ApplicationJson, parameters: parameters);
            ContentType.Parameters.Should().Equal(parameters);
        }

        [Fact]
        public void Single_Parameter_Method_Creates_New_MediaTypeHeaderValue_Instance()
        {
            var initial = new MediaTypeHeaderValue(MediaType.ApplicationJson);
            Builder.SetContent(MediaType.ApplicationJson);
            ContentType.Should().NotBeSameAs(initial);
        }

        [Fact]
        public void MediaTypeHeaderValue_Method_Sets_ContentType_To_Provided_Instance()
        {
            var expected = new MediaTypeHeaderValue("a/b");
            Builder.SetContentType(expected);
            ContentType.Should().BeSameAs(expected);
        }

    }

}
