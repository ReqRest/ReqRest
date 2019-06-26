namespace ReqRest.Builders.Tests.HttpHeadersBuilderExtensions
{
    using System;
    using System.Net.Http.Headers;
    using ReqRest;
    using ReqRest.Builders.Tests.HeaderTestBase;

    public class ConfigureHeadersTests : ConfigureHeadersTestBase<HttpHeaders>
    {

        private readonly IHttpHeadersBuilder _builder = new HttpRequestMessageBuilder();

        protected override HttpHeaders Headers => _builder.Headers;

        protected override void ConfigureHeaders(Action<HttpHeaders> configure) =>
            _builder.ConfigureHeaders(configure);

    }

}
