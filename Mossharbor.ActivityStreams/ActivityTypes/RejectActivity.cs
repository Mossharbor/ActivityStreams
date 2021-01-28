using System;
using System.Collections.Generic;
using System.Text;

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
        internal RejectActivity(string type) : base(type) { }

        public RejectActivity() : base(type: "Reject") { }
    }
}
