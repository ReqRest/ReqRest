namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using ReqRest.Http;
    using ReqRest.Resources;

    /// <summary>
    ///     A collection of <see cref="ResponseTypeInfo"/> instances which ensures that
    ///     no <see cref="ResponseTypeInfo"/> with conflicting status code ranges can be added.
    /// </summary>
    internal class ResponseTypeInfoCollection : Collection<ResponseTypeInfo>
    {

        public ResponseTypeInfoCollection()
            : base() { }

        public ResponseTypeInfoCollection(IList<ResponseTypeInfo> list)
            : base(list) { }

        protected override void SetItem(int index, ResponseTypeInfo item)
        {
            // This method is replacing an old item with new item. We don't care if the item to be
            // replaced conflicts with the new item.
            // -> Don't include it in the checks.
            var items = this.Where((_, i) => i != index);
            VerifyNotConflicting(item, items);
            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, ResponseTypeInfo item)
        {
            VerifyNotConflicting(item, this);
            base.InsertItem(index, item);
        }

        private static void VerifyNotConflicting(ResponseTypeInfo item, IEnumerable<ResponseTypeInfo> items)
        {
            var conflicting = FindConflictingStatusCodes(item, items);

            if (conflicting.Any())
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionStrings.ResponseTypeInfoCollection_ConflictingStatusCodeRanges,
                        FormatConflictingStatusCodes()
                    )
                );
            }

            string FormatConflictingStatusCodes() => 
                string.Join(
                    "\n",
                    conflicting.Select(pair => $"- {pair.Item1} and {pair.Item2}")
                );
        }

        private static IEnumerable<(StatusCodeRange, StatusCodeRange)> FindConflictingStatusCodes(
            ResponseTypeInfo newItem, IEnumerable<ResponseTypeInfo> items) => 
                from info in items
                from currentStatusCode in info.StatusCodes
                from newStatusCode in newItem.StatusCodes
                where newStatusCode.ConflictsWith(currentStatusCode)
                select (currentStatusCode, newStatusCode);

    }

}
