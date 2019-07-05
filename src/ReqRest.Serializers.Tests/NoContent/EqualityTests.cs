namespace ReqRest.Serializers.Tests.NoContent
{
    using FluentAssertions;
    using ReqRest.Serializers;
    using Xunit;

    public class EqualityTests
    {

        [Fact]
        public void Are_Equal_To_Each_Other()
        {
            var a = new NoContent();
            var b = new NoContent();

            a.Equals(b).Should().BeTrue();
            a.Equals((object)b).Should().BeTrue();
            (a == b).Should().BeTrue();
            (!(a != b)).Should().BeTrue();
        }

        [Fact]
        public void Not_Equal_To_Other_Object()
        {
            new NoContent().Equals(new object()).Should().BeFalse();
        }

    }

}
