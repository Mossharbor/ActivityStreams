using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class Tag
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public Uri Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
