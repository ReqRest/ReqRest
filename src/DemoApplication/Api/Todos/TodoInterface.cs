namespace DemoApplication.Api.Todos
{
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

        protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) =>
            baseUrl / "todos" / $"{_id}";

        //public ApiRequestBase<TodoItem> Get() =>
        //    BuildRequest()
        //        .Get()
        //        .ReceiveJson<TodoItem>();

    }

}
