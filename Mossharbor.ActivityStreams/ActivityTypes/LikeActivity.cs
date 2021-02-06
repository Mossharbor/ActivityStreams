using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Like";

        /// <summary>
        /// Initializes a new instance of the <see cref="LikeActivity"/> class.
        /// </summary>
        public LikeActivity() : base(type: TypeString) { }
    }
}
