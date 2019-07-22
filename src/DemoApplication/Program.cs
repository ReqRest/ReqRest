namespace DemoApplication
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DemoApplication.Api;
    using DemoApplication.Api.Todos;
    using Newtonsoft.Json;
    using static System.Console;

    public static class Program
    {

        public static async Task Main()
        {
            // To dive into the code:
            // Start with the JsonPlaceholderClient.cs file.
            // Then follow the comments in there.

            // Using the API wrapper client:
            // Usually, you just create an instance somewhere and (optionally) pass a configuration.
            var client = new JsonPlaceholderClient();

            // Then you can just use the fluent API to follow the REST API:
            var getAllTodosResponse = await client.Todos().Get().FetchResponseAsync();
            var getAllTodosResource = await getAllTodosResponse.DeserializeResourceAsync();

            // An important thing to know is the difference between Response and Resource.
            // 
            // Response means the whole HTTP response data returned from the API, i.e.
            // the status code, the headers, the content, ...
            //
            // Resource means the deserialized .NET POCO that can be parsed from the response's
            // HttpContent.
            // As you can see above, it can be retrieved from an ApiResponse via the
            // DeserializeResourceAsync() method.
            //
            // With the deserialized resource, we, as a user, can now interact with the result
            // returned by the API - without having to worry about anything like status codes!

            if (getAllTodosResource.TryGetValue(out IEnumerable<TodoItem> todos))
            {
                WriteLine($"Fetched {todos.Count()} todos!");

                foreach (var todo in todos)
                {
                    WriteLine($"  - {todo.Title}");
                }
            }
            else
            {
                WriteLine("Failed to load any todos...");
            }
        }

    }

}
