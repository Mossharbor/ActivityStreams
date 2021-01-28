using System;
using System.Text.Json.Serialization;


namespace Mossharbor.ActivityStreams
{
    public class Collection : ActivityObject
    {
        public override string Type
        {
            get
            {
                if (Items != null && Items.Length != 0)
                    return "Collection";

                return "OrderedCollection";
            }
        }


        [JsonPropertyName("totalItems")]
        public uint TotalItems { get; set; }

        [JsonPropertyName("items")]//TODO The Items may be a object or a link combo
        public IActivityObject[] Items { get; set; }

        [JsonPropertyName("orderedItems")]//TODO The OrderedItems may be a object or a link combo
        public IActivityObject[] OrderedItems { get; set; }
    }
}
