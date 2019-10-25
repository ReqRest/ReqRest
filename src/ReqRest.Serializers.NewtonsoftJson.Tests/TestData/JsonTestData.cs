namespace ReqRest.Serializers.NewtonsoftJson.Tests.TestData
{
    using ReqRest.Tests.Sdk.Models;
    using Xunit;

    public class JsonTestData
    {

        public static TheoryData<string> ValidJson { get; } = new TheoryData<string>()
        {
            @"{ 
                ""StringValue"": ""String Value"",
                ""IntValue"": 123, 
                ""DoubleValue"": 123.456, 
                ""NullableInt"": 789 
            }",
            @"{ 
                ""StringValue"": ""String Value"",
                ""IntValue"": 123, 
                ""DoubleValue"": 123.456, 
            }",
            @"{ 
                ""StringValue"": ""String Value"",
                ""IntValue"": 123, 
                ""DoubleValue"": 123.456, 
                ""NullableInt"": null
            }",
            @"{ }",
            @"null",
            @"",
        };

        public static TheoryData<string> InvalidJson { get; } = new TheoryData<string>()
        {
            @"{ ""StringValue"": ""String Value"",",
            @"{{}",
            @"{ ""StringValue"": """"String Value"" }",
        };

        public static TheoryData<SerializationDto?> Dtos { get; } = new TheoryData<SerializationDto?>()
        {
            new SerializationDto()
            {
                StringValue = "String Value",
                IntValue = 123,
                DoubleValue = 123.456,
                NullableInt = 789
            },
            new SerializationDto()
            {
                StringValue = "",
                IntValue = -123,
                DoubleValue = -123.456,
                NullableInt = null,
            },
            new SerializationDto(),
            null,
        };

    }

}
