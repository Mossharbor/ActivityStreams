using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is ignoring the object. The target and origin typically have no defined meaning.
    /// </summary>
    /// <example>
    /// {
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally ignored a note",
    ///  "type": "Ignore",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///   },
    ///   "object": "http://example.org/notes/1"
    /// }
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Ignore"/>
    public class IgnoreActivity : Activity
    {
        public IgnoreActivity() : base(type: "Ignore") { }
    }
}
