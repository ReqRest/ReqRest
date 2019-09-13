namespace ReqRest.Tests.Builders.HttpHeadersBuilderExtensions
{
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.HeaderTestBase;

    public class ClearHeadersTests : ClearHeadersTestBase<HttpHeaders>
    {

        private readonly IHttpHeadersBuilder _builder = new HttpRequestMessageBuilder();

        protected override HttpHeaders Headers => _builder.Headers;

        protected override void ClearHeaders() =>
            _builder.ClearHeaders();

    }

}
