namespace ReqRest.Tests.Builders
{
    using System.Net.Http.Headers;
    using System.Text;
    using ReqRest.Builders;
    using ReqRest.Http;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;

    public partial class HttpHeadersBuilderExtensionsTests
    {

        public class SetContentTypeTests : BuilderTestBase
        {

            [Fact]
            public void MediaType_Sets_Media_Type()
            {
                Service.SetContentType(MediaType.ApplicationJson);
                Assert.Equal(MediaType.ApplicationJson, Service.ContentHeaders.ContentType.MediaType);
            }

            [Fact]
            public void MediaType_CharSet_Sets_Media_Type_And_CharSet()
            {
                var charSet = Encoding.ASCII.WebName;
                Service.SetContentType(MediaType.ApplicationJson, charSet);
                Assert.Equal(charSet, Service.ContentHeaders.ContentType.CharSet);
            }

            [Fact]
            public void MediaType_Parameters_Sets_Parameters()
            {
                var parameters = new[]
                {
                    new NameValueHeaderValue("p1"),
                    new NameValueHeaderValue("p2"),
                };

                Service.SetContentType(MediaType.ApplicationJson, parameters: parameters);
                Assert.Equal(parameters, Service.ContentHeaders.ContentType.Parameters);
            }

            [Fact]
            public void Single_Parameter_Method_Creates_New_MediaTypeHeaderValue_Instance()
            {
                var initial = new MediaTypeHeaderValue(MediaType.ApplicationJson);
                Service.SetStringContent(MediaType.ApplicationJson);
                Assert.NotSame(initial, Service.ContentHeaders.ContentType);
            }

            [Fact]
            public void MediaTypeHeaderValue_Sets_ContentType()
            {
                var expected = new MediaTypeHeaderValue("a/b");
                Service.SetContentType(expected);
                Assert.Same(expected, Service.ContentHeaders.ContentType);
            }

        }

    }

}
