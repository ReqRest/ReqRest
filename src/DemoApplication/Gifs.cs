namespace DemoApplication
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using ReqRest;
    using ReqRest.Builders;
    using DemoApplication.Api.Todos;
    using ReqRest.Serializers.NewtonsoftJson;
    using static System.Console;
    class Foo
    {
        public class TodoItem { }
        public class User { }
        public class ApiError
        {
            public string Description { get; set; }
            public string Code { get; set; }
        }

        public class DemoClient : RestClient
        {
            public UsersInterface Users() => new UsersInterface(this);
            public UserInterface Users(string id) => new UserInterface(this, id);
            public TodosInterface Todos() => new TodosInterface(this);
            public DemoClient() : base(null)
            {
            }
        }

        public class UsersInterface : RestInterface
        {
            public UsersInterface(RestClient client) : base(client) { }
            protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) => baseUrl / "users";
        }

        public class UserInterface : RestInterface
        {
            private string _id;
            public UserInterface(RestClient client, string id) : base(client) { _id = id; }
            protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) => baseUrl / "users" / $"{_id}";
            public ApiRequest<User, ApiError> Get() => throw new NotImplementedException();
            public ApiRequest<User, ApiError> Post(User user) => throw new NotImplementedException();
            public ApiRequest<User, ApiError> Put(User user) => throw new NotImplementedException();
            public ApiRequest<User, ApiError> Patch(User user) => throw new NotImplementedException();
            public TodosInterface Todos() =>
                new TodosInterface(Client, this);
        }

        public class TodosInterface : RestInterface
        {
            public TodosInterface(RestClient client, IUrlProvider? baseUrlProvider = null) : base(client, baseUrlProvider) { }
            protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) => baseUrl / "todos";



            // Code for: [...].Todos().Get(page: 3)
            public ApiRequest<IList<TodoItem>, ApiError> Get(int? page = null) =>
                BuildRequest()
                    .Get()
                    .ConfigureRequestUri(
                        url => url & ("page", $"{page}")
                    )
                    .Receive<IList<TodoItem>>().AsJson(forStatusCodes: 200)
                    .Receive<ApiError>().AsJson(forStatusCodes: (400, 599));










            public ApiRequest<IList<TodoItem>, ApiError> GetFoo(int? page = null) =>
                BuildRequest()
                    .ConfigureRequestUri(
                        url => url & ("page", $"{page}")
                    )
                    .Receive<IList<TodoItem>>().AsJson(forStatusCodes: 200)
                    .Receive<ApiError>().AsJson(forStatusCodes: (400, 599));

            public ApiRequest<TodoItem, ApiError> Post(TodoItem? todoItem) =>
                BuildRequest()
                    .PostJson(todoItem)
                    .Receive<TodoItem>().AsJson(forStatusCodes: 201)
                    .Receive<ApiError>().AsJson(forStatusCodes: (400, 599));
        }

        public class Gifs
        {

            public static async Task Demo()
            {






                var client = new DemoClient();



                var (response, resource) = await client.Users("123").Todos().Get(page: 3).FetchAsync();














                resource.Match(
                    todos => WriteLine($"There are {todos.Count} todos!"),
                    error => WriteLine($"The API returned an error: {error.Description}"),
                    () => WriteLine($"An unexpected status code was returned: {response.StatusCode}")
                );


            }

        }
    }

}
