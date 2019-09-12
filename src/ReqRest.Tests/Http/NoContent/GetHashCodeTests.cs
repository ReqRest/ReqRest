namespace ReqRest.Tests.Http.NoContent
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class GetHashCodeTests
    {

        [Fact]
        public void Share_Same_HashCode()
        {
            var a = new NoContent();
            var b = new NoContent();

            a.GetHashCode().Should().Be(b.GetHashCode());
        }

    }

}
