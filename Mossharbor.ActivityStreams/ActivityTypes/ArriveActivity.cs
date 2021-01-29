using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// An IntransitiveActivity that indicates that the actor has arrived at the location. 
    /// The origin can be used to identify the context from which the actor originated. The target typically has no defined meaning.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally arrived at work",
    ///  "type": "Arrive",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "location": {
    ///    "type": "Place",
    ///    "name": "Work"
    ///  },
    ///  "origin": {
    ///    "type": "Place",
    ///    "name": "Home"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Arrive"/>
    public class ArriveActivity : IntransitiveActivity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Arrive";

        /// <summary>
        /// Initializes a new instance of the <see cref="ArriveActivity"/> class.
        /// </summary>
        public ArriveActivity() : base(type: TypeString) { }
    }
}
