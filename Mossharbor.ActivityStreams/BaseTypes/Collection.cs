using System;
using System.Text.Json.Serialization;


namespace Mossharbor.ActivityStreams
{
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

        [JsonPropertyName("items")]//TODO The Items may be a object or a link combo
        public IActivityObjectOrLink[] Items { get; set; }

        [JsonPropertyName("orderedItems")]//TODO The OrderedItems may be a object or a link combo
        public IActivityObjectOrLink[] OrderedItems { get; set; }
    }
}
