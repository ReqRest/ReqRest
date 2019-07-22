namespace ReqRest.Client.Tests.ResponseTypeInfo
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using ReqRest.Client;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using Xunit;

    public class ToStringTests
    {

        [Theory]
        [MemberData(nameof(ToStringData))]
        public void Returns_Expected_String(Type responseType, IEnumerable<StatusCodeRange> statusCodes, string expected)
        {
            var info = new ResponseTypeInfo(responseType, statusCodes, () => null);
            info.ToString().Should().Be(expected);
        }

        public static TheoryData<Type, IEnumerable<StatusCodeRange>, string> ToStringData { get; } =
            new TheoryData<Type, IEnumerable<StatusCodeRange>, string>()
            {
                { typeof(object), new[] { StatusCodeRange.All }, "Object: (*)" },
                { typeof(Int32), new StatusCodeRange[] { (200, 300) }, "Int32: (200-300)" },
                { typeof(NoContent), new StatusCodeRange[] { (200, 300), 400, StatusCodeRange.All, (null, 100) }, "NoContent: (200-300), (400), (*), (*-100)" },
            };

    }

}
