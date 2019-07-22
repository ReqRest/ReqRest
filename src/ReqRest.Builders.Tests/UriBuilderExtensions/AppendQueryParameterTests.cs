namespace ReqRest.Builders.Tests.UriBuilderExtensions
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class AppendQueryParameterTests : UriBuilderExtensionsTestBase
    {

        [Theory]
        [MemberData(nameof(UriBuilderData.SingleQueryParameterData), MemberType = typeof(UriBuilderData))]
        public void Single_Appends_Query_String(string initialQuery, IEnumerable<string> parameters, string expected)
        {
            Builder.SetQuery(initialQuery);

            foreach (var param in parameters)
            {
                Builder.AppendQueryParameter(param);
            }

            Builder.Query.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(UriBuilderData.KeyValueQueryParameterData), MemberType = typeof(UriBuilderData))]
        public void Key_Value_Appends_Query_String(string initialQuery, string key, string value, string expected)
        {
            Builder.SetQuery(initialQuery);
            Builder.AppendQueryParameter(key, value);
            Builder.Query.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(UriBuilderData.MultipleKeyValueQueryParameterData), MemberType = typeof(UriBuilderData))]
        public void Multiple_Key_Value_Appends_Query_String(
            string initialQuery, IEnumerable<(string, string)> parameters, string expected)
        {
            Builder.SetQuery(initialQuery);
            Builder.AppendQueryParameter(parameters);
            Builder.Query.Should().Be(expected);
        }

    }

}
