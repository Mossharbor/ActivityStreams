using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor dislikes the object.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally disliked a post",
    ///  "type": "Dislike",
    ///  "actor": "http://sally.example.org",
    ///  "object": "http://example.org/posts/1"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Dislike"/>
    public class DislikeActivity : Activity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DislikeActivity"/> class.
        /// </summary>
        public DislikeActivity() : base(type: "Dislike") { }
    }
}
