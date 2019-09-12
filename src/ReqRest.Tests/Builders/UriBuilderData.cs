﻿namespace ReqRest.Tests.Builders
{
    using System.Collections.Generic;
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
        
        public static TheoryData<string, IEnumerable<string>, string> SingleQueryParameterData { get; } =
            new TheoryData<string, IEnumerable<string>, string>()
            {
                { "", new [] { "x=y" }, "?x=y" },
                { "", new [] { "&x=y" }, "?x=y" },
                { "", new [] { "&&x=y" }, "?x=y" },
                { "&", new [] { "x=y" }, "?x=y" },
                { "a=b", new [] { "x=y" }, "?a=b&x=y" },
                { "a=b&", new [] { "x=y" }, "?a=b&x=y" },
                { "a=b", new [] { "&x=y" }, "?a=b&x=y" },
                { "a=b&", new [] { "&x=y" }, "?a=b&x=y" },
                { "a=b&&", new [] { "&&x=y" }, "?a=b&x=y" },
                { "&a=b", new [] { "x=y" }, "?&a=b&x=y" },
                { "&a=b&", new [] { "x=y" }, "?&a=b&x=y" },
                { "&a=b", new [] { "&x=y" }, "?&a=b&x=y" },
                { "&a=b&", new [] { "&x=y" }, "?&a=b&x=y" },
                { "&a=b&&", new [] { "&&x=y" }, "?&a=b&x=y" },
                { "x=y", new string [] {  }, "?x=y" },
                { "x=y", new [] { "" }, "?x=y" },
            };
        
        public static TheoryData<string, string, string, string> KeyValueQueryParameterData { get; } =
            new TheoryData<string, string, string, string>()
            {
                { "", "x", "y", "?x=y" },
                { "&", "x", "y", "?x=y" },
                { "a=b", "x", "y", "?a=b&x=y" },
                { "a=b&", "x", "y", "?a=b&x=y" },
                { "a=b&&", "x", "y", "?a=b&x=y" },
                { "&a=b", "x", "y", "?&a=b&x=y" },
                { "&a=b&", "x", "y", "?&a=b&x=y" },
                { "&a=b&&", "x", "y", "?&a=b&x=y" },
                { "x=y", "", "", "?x=y" },
                { "x=y", null, null, "?x=y" },
            };
        
        public static TheoryData<string, IEnumerable<(string, string)>, string> MultipleKeyValueQueryParameterData { get; } =
            new TheoryData<string, IEnumerable<(string, string)>, string>()
            {
                { "", new [] { ("a", "1") }, "?a=1" },
                { "", new [] { ("a", "1"), ("b", "2") }, "?a=1&b=2" },
                { "", new [] { ("a", "1"), ("b", "2"), ("c", "3") }, "?a=1&b=2&c=3" },
                { "", new [] { ("a", "1"), ("b", "2"), ("c", "3") }, "?a=1&b=2&c=3" },
                { "", new [] { ("a", "1"), ("b", ""), ("c", "3") }, "?a=1&b=&c=3" },
            };



    }

}
