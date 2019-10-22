namespace ReqRest.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Builders.TestData;
    using ReqRest.Tests.Sdk.TestBases;

    public class UriBuilderExtensionsTests
    {

        public class AppendPathTests : TestBase<UriBuilder>
        {

            [Theory]
            [MemberData(nameof(UriBuilderTestData.AppendPathData), MemberType = typeof(UriBuilderTestData))]
            public void Appends_Path(string initialPath, IEnumerable<string?> toAppend, string expected)
            {
                Service.Path = initialPath;

                foreach (var segment in toAppend)
                {
                    Service.AppendPath(segment);
                }

                Assert.Equal(expected, Service.Path);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? segment = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendPath(builder, segment));
            }

        }

        public class AppendQueryParameterTests : TestBase<UriBuilder>
        {

            [Theory]
            [MemberData(nameof(UriBuilderTestData.StringQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void String_Appends_Query_String(string initialQuery, IEnumerable<string?> parameters, string expected)
            {
                Service.SetQuery(initialQuery);

                foreach (var param in parameters)
                {
                    Service.AppendQueryParameter(param);
                }

                Assert.Equal(expected, Service.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.KeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void Key_Value_Appends_Query_String(string initialQuery, string? key, string? value, string expected)
            {
                Service.SetQuery(initialQuery);
                Service.AppendQueryParameter(key, value);
                Assert.Equal(expected, Service.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.MultipleKeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void Key_Value_Tuple_IEnumerable_Appends_Query_String(
                string initialQuery, IEnumerable<(string?, string?)> parameters, string expected)
            {
                Service.SetQuery(initialQuery);
                Service.AppendQueryParameter(parameters);
                Assert.Equal(expected, Service.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.MultipleKeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void Key_Value_Tuple_Array_Appends_Query_String(
                string initialQuery, IEnumerable<(string?, string?)> parameters, string expected)
            {
                Service.SetQuery(initialQuery);
                Service.AppendQueryParameter(parameters.ToArray());
                Assert.Equal(expected, Service.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.MultipleKeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void Key_Value_KeyValuePair_IEnumerable_Appends_Query_String(
                string initialQuery, IEnumerable<(string?, string?)> parameters, string expected)
            {
                Service.SetQuery(initialQuery);
                Service.AppendQueryParameter(parameters.Select(x => new KeyValuePair<string?, string?>(x.Item1, x.Item2)));
                Assert.Equal(expected, Service.Query);
            }

            [Theory]
            [MemberData(nameof(UriBuilderTestData.MultipleKeyValueQueryParameterData), MemberType = typeof(UriBuilderTestData))]
            public void Key_Value_KeyValuePair_Array_IEnumerable_Appends_Query_String(
                string initialQuery, IEnumerable<(string?, string?)> parameters, string expected)
            {
                Service.SetQuery(initialQuery);
                Service.AppendQueryParameter(parameters.Select(x => new KeyValuePair<string?, string?>(x.Item1, x.Item2)).ToArray());
                Assert.Equal(expected, Service.Query);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void String_Throws_ArgumentNullException(UriBuilder builder, string? parameter = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendQueryParameter(builder, parameter));
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null, Null)]
            public void Key_Value_Throws_ArgumentNullException(UriBuilder builder, string? key = null, string? value = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendQueryParameter(builder, key, value));
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Key_Value_Tuple_IEnumerable_Throws_ArgumentNullException(UriBuilder builder, IEnumerable<(string?, string?)>? parameters = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendQueryParameter(builder, parameters));
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Key_Value_KeyValuePair_IEnumerable_Throws_ArgumentNullException(UriBuilder builder, IEnumerable<KeyValuePair<string?, string?>>? parameters = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendQueryParameter(builder, parameters));
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Key_Value_Tuple_Array_Throws_ArgumentNullException(UriBuilder builder, (string?, string?)[]? parameters = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendQueryParameter(builder, parameters));
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Key_Value_KeyValuePair_Array_Throws_ArgumentNullException(UriBuilder builder, KeyValuePair<string?, string?>[]? parameters = null)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.AppendQueryParameter(builder, parameters));
            }

        }

        public class SetFragmentTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("")]
            [InlineData("#")]
            [InlineData("#foo")]
            public void Sets_Fragment(string fragment)
            {
                Service.SetFragment(fragment);
                Assert.Equal(fragment, Service.Fragment);
            }

            [Fact]
            public void Sets_Fragment_To_Empty_String_If_Null()
            {
                Service.SetFragment(null);
                Assert.Equal("", Service.Fragment);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? fragment = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetFragment(builder, fragment));
            }

        }

        public class SetHostTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("")]
            [InlineData("localhost")]
            [InlineData("127.0.0.1")]
            [InlineData("foobar")]
            public void Sets_Host(string host)
            {
                Service.SetHost(host);
                Assert.Equal(host, Service.Host);
            }

            [Fact]
            public void Sets_Host_To_Empty_String_If_Null()
            {
                Service.SetHost(null);
                Assert.Equal("", Service.Host);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? host = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetHost(builder, host));
            }

        }

        public class SetPasswordTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("")]
            [InlineData("Hello")]
            [InlineData("WORLD")]
            public void Sets_Password(string password)
            {
                Service.SetPassword(password);
                Assert.Equal(password, Service.Password);
            }

            [Fact]
            public void Sets_Password_To_Empty_String_If_Null()
            {
                Service.SetPassword(null);
                Assert.Equal("", Service.Password);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? password = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetPassword(builder, password));
            }

        }

        public class SetPathTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("/")]
            [InlineData("//")]
            [InlineData("///")]
            [InlineData("a/b")]
            [InlineData("foo")]
            public void Sets_Path(string path)
            {
                Service.SetPath(path);
                Assert.Equal(path, Service.Path);
            }

            [Fact]
            public void Sets_Path_To_Single_Slash_If_Null()
            {
                Service.SetPath(null);
                Assert.Equal("/", Service.Path);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? path = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetPath(builder, path));
            }

        }

        public class SetPortTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData(-1)]
            [InlineData(0)]
            [InlineData(123)]
            [InlineData(65535)]
            public void Sets_Port_To_Expected_Value(int port)
            {
                Service.SetPort(port);
                Assert.Equal(port, Service.Port);
            }

            [Fact]
            public void Sets_Port_To_Negative_One_If_Null()
            {
                Service.SetPort(null);
                Assert.Equal(-1, Service.Port);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, int? port = 0)
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetPort(builder, port));
            }

        }

        public class SetQueryTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("")]
            [InlineData("?")]
            [InlineData("?foo=bar")]
            public void Sets_Query_To_Expected_Value(string query)
            {
                Service.SetQuery(query);
                Assert.Equal(query, Service.Query);
            }

            [Fact]
            public void Sets_Query_To_Empty_String_If_Null()
            {
                Service.SetQuery(null);
                Assert.Equal("", Service.Query);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? query = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetQuery(builder, query));
            }

        }

        public class SetSchemeTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("")]
            [InlineData("http")]
            [InlineData("FOO")]
            public void Sets_Scheme_To_Expected_Value(string scheme)
            {
                Service.SetScheme(scheme);
                Assert.Equal(scheme, Service.Scheme, ignoreCase: true);
            }

            [Fact]
            public void Sets_Scheme_To_Empty_String_If_Null()
            {
                Service.SetScheme(null);
                Assert.Equal("", Service.Scheme);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? scheme = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetScheme(builder, scheme));
            }

        }

        public class SetUserNameTests : TestBase<UriBuilder>
        {

            [Theory]
            [InlineData("")]
            [InlineData("user")]
            [InlineData("NAME")]
            public void Sets_UserName_To_Expected_Value(string userName)
            {
                Service.SetUserName(userName);
                Assert.Equal(userName, Service.UserName);
            }

            [Fact]
            public void Sets_UserName_To_Empty_String_If_Null()
            {
                Service.SetUserName(null);
                Assert.Equal("", Service.UserName);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(UriBuilder builder, string? userName = "")
            {
                Assert.Throws<ArgumentNullException>(() => UriBuilderExtensions.SetUserName(builder, userName));
            }

        }

    }

}
