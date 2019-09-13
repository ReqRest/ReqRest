namespace ReqRest.Tests.Builders.HttpHeadersBuilderExtensions
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.HeaderTestBase;

    public class SetHeaderTests : SetHeaderTestBase<HttpHeaders>
    {

        private readonly IHttpHeadersBuilder _builder = new HttpRequestMessageBuilder();

        protected override HttpHeaders Headers => _builder.Headers;

        protected override void SetHeader(string name) =>
            _builder.SetHeader(name);

        protected override void SetHeader(string name, string value) =>
            _builder.SetHeader(name, value);

        protected override void SetHeader(string name, IEnumerable<string> values) =>
            _builder.SetHeader(name, values);

    }

}
