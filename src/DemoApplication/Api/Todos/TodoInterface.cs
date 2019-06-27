namespace DemoApplication.Api.Todos
{
    using System;
    using ReqRest.Builders;
    using ReqRest.Client;

    public class TodoInterface : ApiInterface
    {

        private readonly int _id;

        public TodoInterface(ApiClient apiClient, int id)
            : base(apiClient)
        {
            _id = id;
        }

        protected override UriBuilder BuildUrl(UriBuilder baseUrl) =>
            baseUrl.AppendPath($"todos/{_id}");

        //public ApiRequestBase<TodoItem> Get() =>
        //    BuildRequest()
        //        .Get()
        //        .ReceiveJson<TodoItem>();

    }

}
