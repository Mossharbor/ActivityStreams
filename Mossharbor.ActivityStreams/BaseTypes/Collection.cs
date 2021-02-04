using System;
using System.Text.Json.Serialization;


namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A Collection is a subtype of Object that represents ordered or unordered sets of Object or Link instances.
    /// Refer to the Activity Streams 2.0 Core specification for a complete description of the Collection type.
    /// </summary>
    public class Collection : ActivityObject
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string CollectionType = "Collection";

        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string OrderedCollectionType = "OrderedCollection";

        private string type;

        public override string Type
        {
            get
            {
                if (string.IsNullOrEmpty(type))
                {
                    if (Items != null && Items.Length != 0)
                        type = CollectionType;
                    else
                        type = OrderedCollectionType;
                }

                return type;
            }
            set
            {
                type = value;
            }
        }

        public Collection() { }

        public Collection(string type)
        {
            this.type = type;
        }

        private uint totalItems = 0;

        /// <summary>
        /// non-negative integer specifying the total number of objects contained by the
        /// logical view of the collection. This number might not reflect the actual number of items serialized within the Collection object instance
        /// </summary>
        [JsonPropertyName("totalItems")]
        public uint TotalItems
        {
            get
            {
                if (totalItems == 0 && (Items != null || OrderedItems != null))
                {
                    if (Items != null)
                        totalItems = (uint)Items.Length;
                    else
                        totalItems = (uint)OrderedItems.Length;
                }

                return totalItems;
            }
            set
            {
                totalItems = value;
            }
        }

        /// <summary>
        /// In a paged Collection, indicates the furthest preceeding page of items in the collection.
        /// </summary>
        [JsonPropertyName("first")]
        public IActivityObjectOrLink First { get; set; }

        /// <summary>
        /// In a paged Collection, indicates the furthest proceeding page of the collection.
        /// </summary>
        [JsonPropertyName("last")]
        public IActivityObjectOrLink Last { get; set; }

        /// <summary>
        /// In a paged Collection, indicates the page that contains the most recently updated member items.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#current"/>
        [JsonPropertyName("current")]
        public IActivityObjectOrLink Current { get; set; }

        /// <summary>
        /// Identifies the items contained in a collection. The items might be ordered or unordered.
        /// </summary>
        [JsonPropertyName("items")]
        public IActivityObjectOrLink[] Items { get; set; }

        /// <summary>
        /// Identifies the items contained in a collection. The items are ordered.
        /// </summary>
        [JsonPropertyName("orderedItems")]
        public IActivityObjectOrLink[] OrderedItems { get; set; }
    }
}
