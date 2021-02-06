using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has joined the object. The target and origin typically have no defined meaning.
    /// </summary>
    /// <example>
    /// {
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally joined a group",
    ///  "type": "Join",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///   },
    ///   "object": {
    ///     "type": "Group",
    ///     "name": "A Simple Group"
    ///   }
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Join"/>
    public class JoinActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Join";

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinActivity"/> class.
        /// </summary>
        public JoinActivity() : base(type: TypeString) { }
    }
}
