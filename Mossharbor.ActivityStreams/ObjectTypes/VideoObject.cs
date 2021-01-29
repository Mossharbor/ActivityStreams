using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a video document of any kind.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Video",
    ///  "name": "Puppy Plays With Ball",
    ///  "url": "http://example.org/video.mkv",
    ///  "duration": "PT2H"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Video"/>
    public class VideoObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string VideoType = "Video";

        public VideoObject() : base(type: VideoType) { }
    }
}
