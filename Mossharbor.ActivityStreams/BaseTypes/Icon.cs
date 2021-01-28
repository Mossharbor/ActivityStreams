using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class Icon : ImageObject
    {
        [JsonPropertyName("width")]
        public long Width { get; set; }

        [JsonPropertyName("height")]
        public long Height { get; set; }
    }
}
