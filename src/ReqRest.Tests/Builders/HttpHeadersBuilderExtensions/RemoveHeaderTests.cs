﻿namespace ReqRest.Tests.Builders.HttpHeadersBuilderExtensions
{
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.HeaderTestBase;

    public class RemoveHeaderTests : RemoveHeaderTestBase<HttpHeaders>
    {

        private readonly IHttpHeadersBuilder _builder = new HttpRequestMessageBuilder();

        protected override HttpHeaders Headers => _builder.Headers;

        protected override void RemoveHeader(params string[] names) =>
            _builder.RemoveHeader(names);

    }

}