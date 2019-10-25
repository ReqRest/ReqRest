namespace ReqRest.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using ReqRest.Builders;
    using ReqRest.Tests.Builders.TestData;
    using ReqRest.Tests.Sdk.TestBases;

    public class UrlBuilderTests
    {

        public class ConstructorTests
        {

            public static TheoryData<string> Urls { get; } = new TheoryData<string>()
            {
                "https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top",
            };

            [Theory]
            [MemberData(nameof(Urls))]
            public void Initializes_From_String(string url)
            {
                var builder = new UrlBuilder(url);
                AssertDefaultProperties(builder, new Uri(url));
            }

            [Theory]
            [MemberData(nameof(Urls))]
            public void Initializes_From_Uri(string url)
            {
                var uri = new Uri(url);
                var builder = new UrlBuilder(url);
                AssertDefaultProperties(builder, uri);
            }

            private static void AssertDefaultProperties(UrlBuilder builder, Uri url)
            {
                // The values should be equivalent to the base class.
                var reference = new UriBuilder(url);
                Assert.Equal(reference.Scheme, builder.Scheme);
                Assert.Equal(reference.Host, builder.Host);
                Assert.Equal(reference.Path, builder.Path);
                Assert.Equal(reference.Query, builder.Query);
                Assert.Equal(reference.Port, builder.Port);
                Assert.Equal(reference.Fragment, builder.Fragment);
                Assert.Equal(reference.UserName, builder.UserName);
                Assert.Equal(reference.Password, builder.Password);
            }

            [Fact]
            public void Initializes_With_Special_Values()
            {
                var builder = new UrlBuilder(
                    scheme: "https",
                    host: "my-host",
                    port: 1307,
                    path: "test",
                    extraValue: "#fragment"
                );

                Assert.Equal("https", builder.Scheme);
                Assert.Equal("my-host", builder.Host);
                Assert.Equal(1307, builder.Port);
                Assert.Equal("test", builder.Path);
                Assert.Equal("#fragment", builder.Fragment);
            }

        }

        public class AndOperatorTests : TestBase<UrlBuilder>
        {

            [Theory]
            [MemberData(nameof(UriBuilderTestData.StringQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void String_Appends_Query_String(string initialQuery, IEnumerable<string> parameters, string expected)
            {
                var builder = new UrlBuilder().SetQuery(initialQuery);
                foreach (var param in parameters)
                {
                    builder = builder & param;
                }
                Assert.Equal(expected, builder.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.KeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void Tuple_Appends_Query_String(string initialQuery, string key, string value, string expected)
            {
                var builder = new UrlBuilder().SetQuery(initialQuery);
                builder = builder & (key, value);
                Assert.Equal(expected, builder.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.KeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void KeyValuePair_Appends_Query_String(string initialQuery, string key, string value, string expected)
            {
                var builder = new UrlBuilder().SetQuery(initialQuery);
                builder = builder & new KeyValuePair<string?, string?>(key, value);
                Assert.Equal(expected, builder.Query);
            }

        }

        public class DivideOperatorTests
        {

            [Theory]
            [MemberData(nameof(UriBuilderTestData.AppendPathData), MemberType = typeof(UriBuilderTestData))]
            public void Appends_Path_To_End(string initialPath, IEnumerable<string?> toAppend, string expected)
            {
                var builder = new UrlBuilder(path: initialPath);
                foreach (var segment in toAppend)
                {
                    builder = builder / segment;
                }
                Assert.Equal(builder.Path, expected);
            }

        }

    }

}
