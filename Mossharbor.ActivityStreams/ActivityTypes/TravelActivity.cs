using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is traveling to target from origin. 
    /// Travel is an IntransitiveObject whose actor specifies the direct object.
    /// If the target or origin are not specified, either can be determined by context.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally went home from work",
    ///  "type": "Travel",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "target": {
    ///    "type": "Place",
    ///    "name": "Home"
    ///  },
    ///  "origin": {
    ///    "type": "Place",
    ///    "name": "Work"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Travel"/>
    public class TravelActivity : Activity
    {
        public TravelActivity() : base(type: "Travel") { }
    }
}
