namespace ReqRest.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;

    /// <summary>
    ///     Provides static extension methods for the <see cref="HttpHeaders"/> class which
    ///     are shared by all the available builder methods that deal with headers.
    /// </summary>
    internal static class HttpHeadersExtensions
    {

        /// <summary>
        ///     A custom function for adding a header with multiple values which, in comparison to
        ///     the default .NET method, allows empty enumerables.
        /// </summary>
        public static void AddWithUnknownValueCount(this HttpHeaders headers, string name, IEnumerable<string?>? values)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            // We have to work around the .NET API a little bit. See the comment below for details.
            values ??= Enumerable.Empty<string?>();
            values = values.Where(v => !(v is null));

            if (values.Any())
            {
                headers.Add(name, values);
            }
            else
            {
                // According to https://docs.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpheaders.add?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(System.Net.Http.Headers.HttpHeaders.Add)%3Bk(SolutionItemsProject)%3Bk(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8#System_Net_Http_Headers_HttpHeaders_Add_System_String_System_Collections_Generic_IEnumerable_System_String__
                // the HttpRequestMessage doesn't accept null/empty enumerables.
                // The Add(string, string) method does though.
                // -> We can use this one to add a header with an empty value.
                headers.Add(name, "");
            }
        }

        public static void Remove(this HttpHeaders headers, IEnumerable<string?>? names)
        {
            if (names is null) return;

            foreach (var name in names)
            {
                if (!(name is null))
                {
                    headers.Remove(name);
                }
            }
        }

    }

}
