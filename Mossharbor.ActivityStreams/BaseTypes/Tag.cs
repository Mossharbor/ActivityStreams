using System;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
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
