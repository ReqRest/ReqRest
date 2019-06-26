namespace ReqRest.Builders.Tests.HttpRequestPropertiesBuilderExtensions
{
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class RemovePropertyTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [InlineData(new string[] { "Test1" }, new string[] { "Test1" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { "Test1" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { "NotFound" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { null })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { })]
        [InlineData(new string[] { "Test1", "Test2", "Test3" }, null)]
        public void RemoveProperty_Removes_Expected_Properties(string[] initial, string[] toRemove)
        {
            var remainingProperties = initial.ToHashSet();
            remainingProperties.ExceptWith(toRemove ?? new string[0]);

            foreach (var key in initial)
            {
                Builder.AddProperty(key, value: null);
            }

            Builder.RemoveProperty(toRemove);

            foreach (var remaining in remainingProperties)
            {
                Builder.HttpRequestMessage.Properties.Should().ContainKey(remaining);
            }
        }

    }

}
