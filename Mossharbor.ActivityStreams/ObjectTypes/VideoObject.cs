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
        public VideoObject() : base(type: "Video") { }
    }
}
