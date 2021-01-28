using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("altitude")]
        public long Altitude { get; set; }

        [JsonPropertyName("units")]
        public string Units { get; set; }
    }
}
