namespace DemoApplication.Api.Todos
{
    using System.Collections.Generic;
    using ReqRest.Client;
    using ReqRest.Builders;
    using ReqRest.Serializers.NewtonsoftJson;

    public class TodosInterface : ApiInterface
    {

        public TodosInterface(ApiClient apiClient) 
            : base(apiClient) { }

        protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) =>
            baseUrl / "todos";

        public ApiRequest<IEnumerable<TodoItem>> Get() =>
            BuildRequest()
                .Receive<IEnumerable<TodoItem>>().AsJson(200);

        public ApiRequest<TodoItem> Post(TodoItem item) =>
            BuildRequest()
                .Post()
                .SetJsonContent(item)
                .Receive<TodoItem>().AsJson(200);

    }

}
