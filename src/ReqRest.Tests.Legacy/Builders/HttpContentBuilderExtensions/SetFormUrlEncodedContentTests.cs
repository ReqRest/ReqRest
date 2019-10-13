namespace ReqRest.Tests.Builders.HttpContentBuilderExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;
    using HttpRequestMessageBuilder = ReqRest.Builders.HttpRequestMessageBuilder;

    public class SetFormUrlEncodedContentTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void Sets_Content_To_FormUrlEncodedContent()
        {
            Builder.SetFormUrlEncodedContent(("Key", "Value"));
            Builder.HttpRequestMessage.Content.Should().BeOfType<FormUrlEncodedContent>();
        }

        [Theory]
        [MemberData(nameof(ArgumentNullForContentData))]
        public void Throws_Argument_Null_Exception_For_Content(Action method)
        {
            method.Should().Throw<ArgumentNullException>();
        }

        public static TheoryData<Action> ArgumentNullForContentData => new TheoryData<Action>()
        {
            () => new HttpRequestMessageBuilder().SetFormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)null),
            () => new HttpRequestMessageBuilder().SetFormUrlEncodedContent((KeyValuePair<string, string>[])null),
            () => new HttpRequestMessageBuilder().SetFormUrlEncodedContent((IEnumerable<(string, string)>)null),
            () => new HttpRequestMessageBuilder().SetFormUrlEncodedContent(((string, string)[])null),
        };

        // More tests are hard, because there is no easy way to get to the data again.
        // Do that later on.

    }

}
