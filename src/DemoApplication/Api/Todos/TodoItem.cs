namespace DemoApplication.Api.Todos
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Newtonsoft.Json;

    public class TodoItem
    {

        [JsonProperty("userId")]
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonProperty("completed")]
        [JsonPropertyName("completed")]
        public bool? Completed { get; set; }

    }

}
