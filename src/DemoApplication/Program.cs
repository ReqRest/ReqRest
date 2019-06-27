namespace DemoApplication
{
    using System.Threading.Tasks;
    using DemoApplication.Api;
    using Newtonsoft.Json;
    using static System.Console;

    public static class Program
    {

        public static async Task Main()
        {
            var client = new JsonPlaceholderClient();
            var res = await client.Todos().Get().FetchResponseAsync().ConfigureAwait(false);
            var resource = await res.DeserializeResourceAsync().ConfigureAwait(false);

            if ((await res.DeserializeResourceAsync().ConfigureAwait(false)).TryGetValue(out var items))
            {
                foreach (var item in items)
                {
                    WriteLine(JsonConvert.SerializeObject(item));
                }
            }

            //var allRes = await client.Todos().Get().RequestAsync().ConfigureAwait(false);
            //foreach (var item in allRes.Resource)
            //{
            //    WriteLine(item.Id);
            //}

            //WriteLine();

            //var singleRes = await client.Todos(10).Get().RequestAsync().ConfigureAwait(false);
            //var singleItem = singleRes.Resource;
            //WriteLine($"Id: {singleItem.Id}; Title: {singleItem.Title}");

            //var itemToPost = new TodoItem()
            //{
            //    UserId = 1,
            //    Title = "Test post",
            //    IsCompleted = false,
            //};

            //var postRes = await client.Todos().Post(itemToPost).RequestAsync().ConfigureAwait(false);
            //if (postRes.HttpResponseMessage.StatusCode == HttpStatusCode.Created)
            //{
            //    WriteLine("Created. New ID: {0}", postRes.Resource.Id);
            //}
        }

    }

}
