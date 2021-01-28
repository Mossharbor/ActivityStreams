
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class ContentMap
    {
        [JsonPropertyName("en")]
        public string En { get; set; }

        [JsonPropertyName("es")]
        public string Es { get; set; }

        [JsonPropertyName("zh-Hans")]
        public string ZhHans { get; set; }
    }
}
