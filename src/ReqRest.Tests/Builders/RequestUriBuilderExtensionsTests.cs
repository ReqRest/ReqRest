namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class RequestUriBuilderExtensionsTests
    {

        public class ConfigureRequestUriTests : BuilderTestBase
        {

            [Fact]
            public void Action_Creates_UrlBuilder_With_Current_Uri()
            {
                var uri = new Uri("http://foo.bar");
                Service.SetRequestUri(uri);

                Service.ConfigureRequestUri(urlBuilder => 
                { 
                    Assert.Equal(urlBuilder.Uri, uri); 
                });
            }

            [Fact]
            public void Func_UrlBuilder_Creates_UrlBuilder_With_Current_Uri()
            {
                var uri = new Uri("http://foo.bar");
                Service.SetRequestUri(uri);

                Service.ConfigureRequestUri(urlBuilder =>
                {
                    Assert.Equal(urlBuilder.Uri, uri);
                    return urlBuilder;
                });
            }

            [Fact]
            public void Func_Uri_Creates_UrlBuilder_With_Current_Uri()
            {
                var uri = new Uri("http://foo.bar");
                Service.SetRequestUri(uri);

                Service.ConfigureRequestUri(urlBuilder =>
                {
                    Assert.Equal(urlBuilder.Uri, uri);
                    return urlBuilder.Uri;
                });
            }

            [Fact]
            public void Action_Creates_Default_UrlBuilder_If_RequestUri_Is_Null()
            {
                var expected = new UrlBuilder().Uri;
                Service.ConfigureRequestUri(urlBuilder =>
                {
                    Assert.Equal(expected, urlBuilder.Uri);
                });
            }

            [Fact]
            public void Func_UrlBuilder_Creates_Default_UrlBuilder_If_RequestUri_Is_Null()
            {
                var expected = new UrlBuilder().Uri;
                Service.ConfigureRequestUri(urlBuilder =>
                {
                    Assert.Equal(expected, urlBuilder.Uri);
                    return urlBuilder;
                });
            }

            [Fact]
            public void Func_Uri_Creates_Default_UrlBuilder_If_RequestUri_Is_Null()
            {
                var expected = new UrlBuilder().Uri;
                Service.ConfigureRequestUri(urlBuilder =>
                {
                    Assert.Equal(expected, urlBuilder.Uri);
                    return urlBuilder.Uri;
                });
            }

            [Fact]
            public void Action_Calls_Configure()
            {
                var wasCalled = false;
                Service.ConfigureRequestUri(urlBuilder => wasCalled = true);
                Assert.True(wasCalled);
            }

            [Fact]
            public void Func_UrlBuilder_Calls_Configure()
            {
                var wasCalled = false;
                Service.ConfigureRequestUri(urlBuilder => { wasCalled = true; return urlBuilder; });
                Assert.True(wasCalled);
            }

            [Fact]
            public void Func_Uri_Calls_Configure()
            {
                var wasCalled = false;
                Service.ConfigureRequestUri(urlBuilder => { wasCalled = true; return urlBuilder.Uri; });
                Assert.True(wasCalled);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Action_Throws_ArgumentNullException(IRequestUriBuilder builder, Action<UrlBuilder> configure)
            {
                Assert.Throws<ArgumentNullException>(() => RequestUriBuilderExtensions.ConfigureRequestUri(builder, configure));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Func_UrlBuilder_Throws_ArgumentNullException(IRequestUriBuilder builder, Func<UrlBuilder, UriBuilder?> configure)
            {
                Assert.Throws<ArgumentNullException>(() => RequestUriBuilderExtensions.ConfigureRequestUri(builder, configure));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Func_Uri_Throws_ArgumentNullException(IRequestUriBuilder builder, Func<UrlBuilder, Uri?> configure)
            {
                Assert.Throws<ArgumentNullException>(() => RequestUriBuilderExtensions.ConfigureRequestUri(builder, configure));
            }

        }

        public class SetRequestUriTests : BuilderTestBase
        {

            public static TheoryData<string?> UriData { get; } = new TheoryData<string?>()
            {
                "http://foo.bar",
                "/relative",
                null,
            };

            [Theory, MemberData(nameof(UriData))]
            public void String_Sets_RequestUri(string? requestUri)
            {
                Service.SetRequestUri(requestUri);
                Assert.Equal(requestUri, Service.RequestUri?.OriginalString);
            }
            
            [Theory, MemberData(nameof(UriData))]
            public void Uri_Sets_RequestUri(string? requestUri)
            {
                var uri = requestUri is null ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute);
                Service.SetRequestUri(uri);
                Assert.Equal(uri, Service.RequestUri);
            }

            [Fact]
            public void String_Uses_UriKind()
            {
                // The easiest way to ensure this is to specify a different-than-default UriKind
                // and wait for an exception.
                Assert.Throws<UriFormatException>(
                    () => Service.SetRequestUri("../relative", UriKind.Absolute)
                );
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void String_Throws_ArgumentNullException(IRequestUriBuilder builder, string? requestUri = "http://foo.bar")
            {
                Assert.Throws<ArgumentNullException>(() => RequestUriBuilderExtensions.SetRequestUri(builder, requestUri));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Uri_Throws_ArgumentNullException(IRequestUriBuilder builder, string? requestUri = "http://foo.bar")
            {
                var uri = requestUri is null ? null : new Uri(requestUri, UriKind.RelativeOrAbsolute);
                Assert.Throws<ArgumentNullException>(() => RequestUriBuilderExtensions.SetRequestUri(builder, uri));
            }

        }

    }

}
