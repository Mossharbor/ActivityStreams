using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class ActivityLink : IActivityLink
    //TODO implment parsing so items can be links or objects.
    {
        public ActivityLink()
        {
            this.Type = "Link";
        }
        protected ActivityLink(string type)
        {
            this.Type = type;
        }

        [JsonPropertyName("href")]
        public string HREF { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("rel")]
        public string[] Rel { get; set; }

        [JsonPropertyName("mediaType")]
        public string mediaType { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("width")]
        public long Width { get; set; }

        [JsonPropertyName("height")]
        public long Height { get; set; }

    }
}
