namespace ReqRest.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using ReqRest.Http;
    using ReqRest.Resources;

    /// <summary>
    ///     A collection of <see cref="ResponseTypeDescriptor"/> instances which ensures that
    ///     no <see cref="ResponseTypeDescriptor"/> with conflicting status code ranges can be added.
    /// </summary>
    internal class ResponseTypeDescriptorCollection : Collection<ResponseTypeDescriptor>
    {

        public ResponseTypeDescriptorCollection()
            : base() { }

        public ResponseTypeDescriptorCollection(IList<ResponseTypeDescriptor> list)
            : base(list) { }

        protected override void SetItem(int index, ResponseTypeDescriptor item)
        {
            // This method is replacing an old item with new item. We don't care if the item to be
            // replaced conflicts with the new item.
            // -> Don't include it in the checks.
            var items = this.Where((_, i) => i != index);
            VerifyNotConflicting(item, items);
            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, ResponseTypeDescriptor item)
        {
            VerifyNotConflicting(item, this);
            base.InsertItem(index, item);
        }

        private static void VerifyNotConflicting(ResponseTypeDescriptor item, IEnumerable<ResponseTypeDescriptor> items)
        {
            var conflicting = FindConflictingStatusCodes(item, items);
            if (conflicting.Any())
            {
                throw new InvalidOperationException(
                    ExceptionStrings.ResponseTypeDescriptorCollection_ConflictingStatusCodeRanges(conflicting)
                );
            }
        }

        private static IEnumerable<(StatusCodeRange, StatusCodeRange)> FindConflictingStatusCodes(
            ResponseTypeDescriptor newItem, IEnumerable<ResponseTypeDescriptor> items) => 
                from descriptor in items
                from currentStatusCode in descriptor.StatusCodes
                from newStatusCode in newItem.StatusCodes
                where newStatusCode.ConflictsWith(currentStatusCode)
                select (currentStatusCode, newStatusCode);

    }

}
