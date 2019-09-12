﻿namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.HeaderTestBase;

    public class ClearContentHeadersTests : ClearHeadersTestBase<HttpContentHeaders>
    {

        private readonly IHttpContentBuilder _builder = new HttpRequestMessageBuilder()
        {
            HttpRequestMessage = new HttpRequestMessage()
            {
                Content = new ByteArrayContent(new byte[0]),
            },
        };

        protected override HttpContentHeaders Headers => _builder.Content.Headers;

        protected override void ClearHeaders() =>
            _builder.ClearContentHeaders();

    }

}
