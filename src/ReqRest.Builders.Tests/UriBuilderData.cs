namespace ReqRest.Builders.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Xunit;

    public static class UriBuilderData
    {

        public static TheoryData<string, IEnumerable<string>, string> AppendPathData { get; } =
            new TheoryData<string, IEnumerable<string>, string>()
            {
                // Multiple subsequent paths.
                { "/", new string [] { null }, "//" },
                { "/", new string [] { "" }, "//" },
                { "/one", new string [] { null }, "/one/" },
                { "/one", new string [] { "" }, "/one/" },

                // Default appending.
                { "/", new [] { "one" }, "/one" },
                { "/", new [] { "one/two" }, "/one/two" },
                { "/", new [] { "one", "two" }, "/one/two" },
                { "/", new [] { "one", "two", "three" }, "/one/two/three" },

                { "one", new [] { "two" }, "one/two" },
                { "one", new [] { "two", "three" }, "one/two/three" },
                { "one/two", new [] { "three" }, "one/two/three" },

                // Single path separators in segment(s).
                { "/", new [] { "/one" }, "/one" },
                { "/", new [] { "one/", "two" }, "/one/two" },
                { "/", new [] { "one", "/two" }, "/one/two" },
                { "/", new [] { "one/", "/two" }, "/one/two" },

                { "one", new[] { "//" }, "one//" },
                { "one/", new[] { "//" }, "one//" },
                { "one/", new[] { "//" }, "one//" },
                { "one//", new[] { "two" }, "one//two" },
                { "one//", new[] { "/two" }, "one//two" },
                { "one//", new[] { "//two" }, "one///two" },
                { "one//", new[] { "/" }, "one//" },
            };

    }

}
