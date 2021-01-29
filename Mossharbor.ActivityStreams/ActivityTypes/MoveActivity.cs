using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has moved object from origin to target. If the origin or target are not specified, either can be determined by context.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally moved a post from List A to List B",
    ///  "type": "Move",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": "http://example.org/posts/1",
    ///  "target": {
    ///    "type": "Collection",
    ///    "name": "List B"
    ///  },
    ///  "origin": {
    ///    "type": "Collection",
    ///    "name": "List A"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Move"/>
    public class MoveActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Move";

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveActivity"/> class.
        /// </summary>
        public MoveActivity() : base(type: TypeString) { }
    }
}
