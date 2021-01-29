using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has viewed the object.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally read an article",
    ///  "type": "View",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Article",
    ///    "name": "What You Should Know About Activity Streams"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#View"/>
    public class ViewActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "View";

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewActivity"/> class.
        /// </summary>
        public ViewActivity() : base(type: TypeString) { }
    }
}
