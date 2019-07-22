namespace DemoApplication.Api.Todos
{
    using Newtonsoft.Json;

    public class TodoItem
    {

        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("completed")]
        public bool? IsCompleted { get; set; }

    }

}
