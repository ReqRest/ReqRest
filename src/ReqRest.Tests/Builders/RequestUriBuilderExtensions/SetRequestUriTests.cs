namespace ReqRest.Tests.Builders.RequestUriBuilderExtensions
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetRequestUriTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [MemberData(nameof(SetRequestUriWithUriData))]
        public void SetRequestUri_With_Uri_Sets_RequestUri(Uri requestUri)
        {
            Builder.SetRequestUri(requestUri);
            Builder.HttpRequestMessage.RequestUri.Should().BeSameAs(requestUri);
        }

        public static TheoryData<Uri> SetRequestUriWithUriData => new TheoryData<Uri>()
            { null, new Uri("http://test.com") };


        [Theory]
        [MemberData(nameof(SetRequestUriWithStringData))]
        public void SetRequestUri_With_String_Sets_RequestUri(string requestUriStr)
        {
            Builder.SetRequestUri(requestUriStr);
            Builder.HttpRequestMessage.RequestUri?.OriginalString.Should().BeSameAs(requestUriStr);
        }

        public static TheoryData<string> SetRequestUriWithStringData => new TheoryData<string>()
            { null, "http://test.com" };

    }

}
