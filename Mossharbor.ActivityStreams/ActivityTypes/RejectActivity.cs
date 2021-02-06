using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is rejecting the object. The target and origin typically have no defined meaning.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally rejected an invitation to a party",
    ///  "type": "Reject",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Invite",
    ///    "actor": "http://john.example.org",
    ///    "object": {
    ///        "type": "Event",
    ///      "name": "Going-Away Party for Jim"
    ///    }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Reject"/>
    public class RejectActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string RejectActivityTypeString = "Reject";

        /// <summary>
        /// Initializes a new instance of the <see cref="RejectActivity"/> class.
        /// </summary>
        /// <param name="type">the type of the activity</param>
        protected RejectActivity(string type) : base(type) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RejectActivity"/> class.
        /// </summary>
        public RejectActivity() : base(type: RejectActivityTypeString) { }
    }
}
