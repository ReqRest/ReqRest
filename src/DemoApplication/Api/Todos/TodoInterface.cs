namespace DemoApplication.Api.Todos
{
    using ReqRest.Builders;
    using ReqRest;
    using ReqRest.Serializers.NewtonsoftJson;

    public class TodoInterface : RestInterface
    {

        private readonly int _id;

        public TodoInterface(RestClient restClient, int id)
            : base(restClient)
        {
            _id = id;
        }

        protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) =>
            baseUrl / "todos" / $"{_id}";

        public ApiRequest<TodoItem> Get() => 
            BuildRequest()
                .Get()
                .Receive<TodoItem>().AsJson(200);

    }

}
