using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A specialization of Reject in which the rejection is considered tentative.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally tentatively rejected an invitation to a party",
    ///  "type": "TentativeReject",
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
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#TentativeReject"/>
    public class TentativeRejectActivity : RejectActivity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "TentativeReject";

        /// <summary>
        /// Initializes a new instance of the <see cref="TentativeRejectActivity"/> class.
        /// </summary>
        public TentativeRejectActivity() : base(type: TypeString) { }
    }
}
