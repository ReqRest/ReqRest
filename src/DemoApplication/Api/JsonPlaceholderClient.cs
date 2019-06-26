namespace DemoApplication.Api
{
    using System;
    using DemoApplication.Api.Todos;
    using ReqRest;

    public class JsonPlaceholderClient : ApiClient
    {

        public JsonPlaceholderClient()
            : base(Configure()) { }

        private static ApiClientConfiguration Configure() => new ApiClientConfiguration
        {
            BaseUrl = new Uri("https://jsonplaceholder.typicode.com")
        };

        public TodosInterface Todos() =>
            new TodosInterface(this);

        public TodoInterface Todos(int id) =>
            new TodoInterface(this, id);

    }

}
