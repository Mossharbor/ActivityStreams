using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has read the object.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally read a blog post",
    ///  "type": "Read",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": "http://example.org/posts/1"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Read"/>
    public class ReadActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Read";

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadActivity"/> class.
        /// </summary>
        public ReadActivity() : base(type: TypeString) { }
    }
}
