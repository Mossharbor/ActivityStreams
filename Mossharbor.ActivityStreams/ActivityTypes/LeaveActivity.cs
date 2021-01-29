using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has left the object. The target and origin typically have no meaning.
    /// </summary>
    /// <example>
    /// {
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally left work",
    ///  "type": "Leave",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///   },
    ///   "object": {
    ///     "type": "Group",
    ///     "name": "A Simple Group"
    ///   }
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Leave"/>
    public class LeaveActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Leave";

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnounceActivity"/> class.
        /// </summary>
        public LeaveActivity() : base(type: TypeString) { }
    }
}
