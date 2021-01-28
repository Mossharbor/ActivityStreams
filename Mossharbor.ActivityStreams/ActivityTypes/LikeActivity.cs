using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// {
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally Liked a note",
    ///  "type": "Like",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///   },
    ///   "object": "http://example.org/notes/1"
    /// }
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Like"/>
    public class LikeActivity : Activity
    {
        public LikeActivity() : base(type: "Like") { }
    }
}
