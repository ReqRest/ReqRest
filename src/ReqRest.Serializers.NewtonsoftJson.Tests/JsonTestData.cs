namespace ReqRest.Serializers.NewtonsoftJson.Tests
{
    using Xunit;

    public class JsonTestData
    {

        public static TheoryData<string> ValidJson { get; } = new TheoryData<string>()
        {
            @"{ 
                ""stringValue"": ""String Value"",
                ""intValue"": 123, 
                ""doubleValue"": 123.456, 
                ""nullableInt"": 789 
            }",
            @"{ 
                ""stringValue"": ""String Value"",
                ""intValue"": 123, 
                ""doubleValue"": 123.456, 
            }",
            @"{ 
                ""stringValue"": ""String Value"",
                ""intValue"": 123, 
                ""doubleValue"": 123.456, 
                ""nullableInt"": null
            }",
            @"{ }",
            @"null",
        };

        public static TheoryData<string> InvalidJson { get; } = new TheoryData<string>()
        {
            @"{ ""stringValue"": ""String Value"",",
            @"{{}",
            @"{ ""stringValue"": """"String Value"" }",
        };

        public static TheoryData<MockDto> Dtos { get; } = new TheoryData<MockDto>()
        {
            new MockDto()
            {
                StringValue = "String Value",
                IntValue = 123,
                DoubleValue = 123.456,
                NullableInt = 789
            },
            new MockDto()
            {
                StringValue = "",
                IntValue = -123,
                DoubleValue = -123.456,
                NullableInt = null,
            },
            new MockDto(),
            null,
        };

    }

}
