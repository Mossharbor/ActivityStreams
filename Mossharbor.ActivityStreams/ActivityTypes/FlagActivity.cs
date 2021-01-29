using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is "flagging" the object. Flagging is defined in the sense common to many social platforms as reporting content as being inappropriate for any number of reasons.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally flagged an inappropriate note",
    ///  "type": "Flag",
    ///  "actor": "http://sally.example.org",
    ///  "object": {
    ///    "type": "Note",
    ///    "content": "An inappropriate note"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Flag"/>
    public class FlagActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Flag";

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagActivity"/> class.
        /// </summary>
        public FlagActivity() : base(type: TypeString) { }
    }
}
