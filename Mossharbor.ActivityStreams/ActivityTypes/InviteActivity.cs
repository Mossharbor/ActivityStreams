using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A specialization of Offer in which the actor is extending an invitation for the object to the target.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally invited John and Lisa to a party",
    ///  "type": "Invite",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Event",
    ///    "name": "A Party"
    ///  },
    ///  "target": [
    ///    {
    ///      "type": "Person",
    ///      "name": "John"
    ///    },
    ///    {
    ///    "type": "Person",
    ///      "name": "Lisa"
    ///    }
    ///  ]
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Invite"/>
    public class InviteActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Invite";

        /// <summary>
        /// Initializes a new instance of the <see cref="InviteActivity"/> class.
        /// </summary>
        public InviteActivity() : base(type: TypeString) { }
    }
}
