using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class Icon : ImageObject, ICustomParser
    {
        public const string IconType = "Icon";

        /// <summary>
        /// the icon height
        /// </summary>
        [JsonPropertyName("width")]
        public long Width { get; set; }

        /// <summary>
        /// the icon width
        /// </summary>
        [JsonPropertyName("height")]
        public long Height { get; set; }

        /// <summary>
        /// Parses out the details specific to the Place Object
        /// </summary>
        /// <param name="el"></param>
        public override void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            this.Width = el.GetLongOrDefault("width");
            this.Height = el.GetLongOrDefault("height");
        }
    }
}
