namespace ReqRest.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpRequestPropertiesBuilderExtensionsTests
    {

        public class AddPropertyTests : BuilderTestBase
        {

            [Theory]
            [InlineData("Test", "Value")]
            [InlineData("Test", 123)]
            [InlineData("Test", null)]
            public void Adds_Property(string key, object? value)
            {
                Service.AddProperty(key, value);
                Assert.Equal(value, Service.Properties[key]);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull, Null)]
            public void Throws_ArgumentNullException(IHttpRequestPropertiesBuilder builder, string key = "Test", object? value = null)
            {
                Assert.Throws<ArgumentNullException>(() => HttpRequestPropertiesBuilderExtensions.AddProperty(builder, key, value));
            }

            [Fact]
            public void Throws_ArgumentException_If_Key_Already_Exists()
            {
                Service.AddProperty("Key", null);
                Assert.Throws<ArgumentException>(() => Service.AddProperty("Key", null));
            }

        }

        public class ClearPropertiesTests : BuilderTestBase
        {

            [Fact]
            public void ClearProperties_Clears_Properties()
            {
                Service
                    .AddProperty("Test", "Property")
                    .ClearProperties();
                Assert.Empty(Service.Properties);
            }

        }
        
        public class ConfigurePropertiesTests : BuilderTestBase
        {

            [Fact]
            public void Invokes_Action()
            {
                var wasCalled = false;
                Service.ConfigureProperties(_ => wasCalled = true);
                Assert.True(wasCalled);
            }

            [Fact]
            public void Passes_Properties()
            {
                Service.ConfigureProperties(
                    properties => Assert.Same(Service.Properties, properties)
                );
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(IHttpRequestPropertiesBuilder builder, Action<IDictionary<string, object?>> configure)
            {
                Assert.Throws<ArgumentNullException>(() => HttpRequestPropertiesBuilderExtensions.ConfigureProperties(builder, configure));
            }

        }

        public class RemovePropertyTests : BuilderTestBase
        {

            [Theory]
            [InlineData(new string[] { "Test1" }, new string?[] { "Test1" })]
            [InlineData(new string[] { "Test1", "Test2" }, new string?[] { "Test1" })]
            [InlineData(new string[] { "Test1", "Test2" }, new string?[] { "NotFound" })]
            [InlineData(new string[] { "Test1", "Test2" }, new string?[] { null })]
            [InlineData(new string[] { "Test1", "Test2" }, new string?[] { })]
            [InlineData(new string[] { "Test1", "Test2", "Test3" }, null)]
            public void Removes_Expected_Properties(string[] initial, string?[]? toRemove)
            {
                var remainingProperties = initial.ToHashSet();
                remainingProperties.ExceptWith((toRemove ?? Array.Empty<string?>())!);

                foreach (var key in initial)
                {
                    Service.AddProperty(key, value: null);
                }

                Service.RemoveProperty(toRemove);

                foreach (var remaining in remainingProperties)
                {
                    Assert.True(Service.Properties.ContainsKey(remaining));
                }
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(IHttpRequestPropertiesBuilder builder, string?[]? names = null)
            {
                Assert.Throws<ArgumentNullException>(() => HttpRequestPropertiesBuilderExtensions.RemoveProperty(builder, names));
            }

        }

        public class SetPropertyTests : BuilderTestBase
        {

            [Theory]
            [InlineData("Test", "Value")]
            [InlineData("Test", null)]
            public void SetProperty_Adds_Property(string key, object value)
            {
                Service.SetProperty(key, value);
                Assert.Equal(value, Service.Properties[key]);
            }

            [Fact]
            public void SetProperty_Overwrites_If_Key_Already_Exists()
            {
                var key = "Key";
                Service.SetProperty(key, new object());
                Service.SetProperty(key, 123);
                Assert.Equal(123, Service.Properties[key]);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull, Null)]
            public void Throws_ArgumentNullException(IHttpRequestPropertiesBuilder builder, string key = "Test", object? value = null)
            {
                Assert.Throws<ArgumentNullException>(() => HttpRequestPropertiesBuilderExtensions.SetProperty(builder, key, value));
            }

        }

    }

}
