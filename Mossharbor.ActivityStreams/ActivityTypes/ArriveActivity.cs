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
        public ArriveActivity() : base(type: "Arrive") { }
    }
}
