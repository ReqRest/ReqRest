namespace ReqRest.Tests.Builders.UrlBuilder
{
    using System.Collections.Generic;
    using Xunit;
    using ReqRest.Builders;
    using FluentAssertions;

    public class AndOperatorTests
    {

        [Theory]
        [MemberData(nameof(UriBuilderData.SingleQueryParameterData), MemberType = typeof(UriBuilderData))]
        public void Single_Appends_Query_String(string initialQuery, IEnumerable<string> parameters, string expected)
        {
            var builder = new UrlBuilder().SetQuery(initialQuery);
            
            foreach (var param in parameters)
            {
                builder = builder & param;
            }

            builder.Query.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(UriBuilderData.KeyValueQueryParameterData), MemberType = typeof(UriBuilderData))]
        public void Key_Value_Appends_Query_String_With_ValueTuple(string initialQuery, string key, string value, string expected)
        {
            var builder = new UrlBuilder().SetQuery(initialQuery);
            builder = builder & (key, value);
            builder.Query.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(UriBuilderData.KeyValueQueryParameterData), MemberType = typeof(UriBuilderData))]
        public void Key_Value_Appends_Query_String_With_KeyValuePair(string initialQuery, string key, string value, string expected)
        {
            var builder = new UrlBuilder().SetQuery(initialQuery);
            builder = builder & new KeyValuePair<string, string>(key, value);
            builder.Query.Should().Be(expected);
        }

    }

}
