using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents an audio document of any kind.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Audio",
    ///  "name": "Interview With A Famous Technologist",
    ///  "url": {
    ///    "type": "Link",
    ///    "href": "http://example.org/podcast.mp3",
    ///    "mediaType": "audio/mp3"
    ///  }
    ///}
    /// </example>
    /// <see cref="	https://www.w3.org/ns/activitystreams#Audio"/>
    public class AudioObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string AudioType = "Audio";

        public AudioObject() : base(type: "AudioType") { }
    }
}
