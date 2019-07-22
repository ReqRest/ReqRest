namespace ReqRest.Serializers.NewtonsoftJson.Tests
{
    using Newtonsoft.Json;

    public class MockDto
    {

        [JsonProperty("stringValue")]
        public string StringValue { get; set; }

        [JsonProperty("intValue")]
        public int IntValue { get; set; }

        [JsonProperty("doubleValue")]
        public double DoubleValue { get; set; }

        [JsonProperty("nullableInt")]
        public int? NullableInt { get; set; }

    }

}
