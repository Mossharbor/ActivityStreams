using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class ActivityLink : IActivityLink
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string ActivityLinkType = "Link";

        public ActivityLink()
        {
            this.Type = ActivityLinkType;
        }
        protected ActivityLink(string type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Identifies the context within which the object exists or an activity was performed.
        /// The notion of "context" used is intentionally vague.The intended function is to serve as a 
        /// means of grouping objects and activities that share a common originating context or purpose.
        /// An example could be all activities relating to a common project or event.
        /// </summary>
        /// <see cref="	https://www.w3.org/ns/activitystreams#context"/>
        [JsonPropertyName("@context")]
        public Uri Context { get; set; }

        /// <summary>
        /// Hints as to the language used by the target resource. Value must be a [BCP47] Language-Tag.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Link",
        ///  "href": "http://example.org/abc",
        ///  "hreflang": "en",
        ///  "mediaType": "text/html",
        ///  "name": "Previous"
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#hreflang"/>
        [JsonPropertyName("hreflang")]
        public string HrefLang { get; set; }

        /// <summary>
        /// The target resource pointed to by a Link.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Link",
        ///  "href": "http://example.org/abc",
        ///  "hreflang": "en",
        ///  "mediaType": "text/html",
        ///  "name": "Previous"
        ///}
        /// </example>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// A link relation associated with a Link. The value must conform to both the [HTML5] and [RFC5988] "link relation" definitions.
        /// In the[HTML5], any string not containing the "space" U+0020, "tab" (U+0009), "LF" (U+000A), "FF" (U+000C), "CR" (U+000D) or "," (U+002C) characters can be used as a valid link relation.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Link",
        ///  "href": "http://example.org/abc",
        ///  "hreflang": "en",
        ///  "mediaType": "text/html",
        ///  "name": "Preview",
        ///  "rel": ["canonical", "preview"]
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#rel"/>
        [JsonPropertyName("rel")]
        public string[] Rel { get; set; }

        [JsonPropertyName("mediaType")]
        public string MediaType { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("width")]
        public long Width { get; set; }

        [JsonPropertyName("height")]
        public long Height { get; set; }

    }
}
