namespace DemoApplication.Api.Todos
{
    using System;
    using System.Collections.Generic;
    using ReqRest;

    public class TodosInterface : ApiInterface
    {

        public TodosInterface(ApiClient apiClient) 
            : base(apiClient) { }

        protected override UriBuilder BuildUrl(UriBuilder baseUrl) =>
            baseUrl.AppendPath("todos");

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
