using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Travel";

        /// <summary>
        /// Initializes a new instance of the <see cref="TravelActivity"/> class.
        /// </summary>
        public TravelActivity() : base(type: TypeString) { }
    }
}
