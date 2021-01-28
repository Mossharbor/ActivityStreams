using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has listened to the object.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally listened to a piece of music",
    ///  "type": "Listen",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": "http://example.org/music.mp3"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Listen"/>
    public class ListenActivity : Activity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenActivity"/> class.
        /// </summary>
        public ListenActivity() : base(type: "Listen") { }
    }
}
