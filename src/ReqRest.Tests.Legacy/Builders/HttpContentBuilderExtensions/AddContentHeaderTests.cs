namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.HeaderTestBase;

    public class AddContentHeaderTests : AddHeaderTestBase<HttpContentHeaders>
    {

        private readonly IHttpContentBuilder _builder = new HttpRequestMessageBuilder()
        {
            HttpRequestMessage = new HttpRequestMessage()
            {
                Content = new ByteArrayContent(new byte[0]),
            },
        };

        protected override HttpContentHeaders Headers => _builder.Content.Headers;

        protected override void AddHeader(string name) =>
            _builder.AddContentHeader(name);

        protected override void AddHeader(string name, string value) =>
            _builder.AddContentHeader(name, value);

        protected override void AddHeader(string name, IEnumerable<string> values) =>
            _builder.AddContentHeader(name, values);

    }

}
