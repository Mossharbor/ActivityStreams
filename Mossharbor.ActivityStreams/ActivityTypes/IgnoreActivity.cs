using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Ignore";

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreActivity"/> class.
        /// </summary>
        public IgnoreActivity() : base(type: TypeString) { }
    }
}
