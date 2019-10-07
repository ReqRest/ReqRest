namespace ReqRest.Serializers.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Xunit;
    using System.Text.Json;
    using ReqRest.Serializers.Json.Tests.Attributes;

    public class JsonHttpContentSerializerTests
    {

        public class Constructor
        {

            [Theory, NullAndDefaultData]
            public void Sets_JsonSerializerOptions(JsonSerializerOptions? options)
            {
                var serializer = new JsonHttpContentSerializer(options);
                Assert.Same(options, serializer.JsonSerializerOptions);
            }

        }

        public class Serialize
        {

        }

    }

}
