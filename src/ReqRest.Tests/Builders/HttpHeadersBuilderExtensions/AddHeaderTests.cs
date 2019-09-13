namespace ReqRest.Tests.Builders.HttpHeadersBuilderExtensions
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.HeaderTestBase;

    public class AddHeaderTests : AddHeaderTestBase<HttpHeaders>
    {

        private readonly IHttpHeadersBuilder _builder = new HttpRequestMessageBuilder();

        protected override HttpHeaders Headers => _builder.Headers;

        protected override void AddHeader(string name) =>
            _builder.AddHeader(name);

        protected override void AddHeader(string name, string value) =>
            _builder.AddHeader(name, value);

        protected override void AddHeader(string name, IEnumerable<string> values) =>
            _builder.AddHeader(name, values);

    }

}
