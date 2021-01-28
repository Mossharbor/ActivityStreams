using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class CollectionPage : Collection
    {
        [JsonPropertyName("partOf")]//TODO The Items may be a object or a link combo
        public ActivityObject PartOf { get; set; }

        [JsonPropertyName("next")]//TODO The Next may be a string or a link combo
        public CollectionPage Next { get; set; }

        [JsonPropertyName("prev")]//TODO The Prev may be a string or a link combo
        public CollectionPage Prev { get; set; }
    }
}
