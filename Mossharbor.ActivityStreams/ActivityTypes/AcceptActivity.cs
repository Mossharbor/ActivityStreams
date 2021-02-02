
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor accepts the object. The target property can be used in certain circumstances to indicate the context into which the object has been accepted.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally accepted an invitation to a party",
    ///  "type": "Accept",
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
    /// <example>
    ///{
    ///    "@context": "https://www.w3.org/ns/activitystreams",
    ///    "summary": "Sally accepted Joe into the club",
    ///    "type": "Accept",
    ///    "actor": {
    ///      "type": "Person",
    ///      "name": "Sally"
    ///    },
    ///    "object": {
    ///    "type": "Person",
    ///      "name": "Joe"
    ///    },
    ///    "target": {
    ///    "type": "Group",
    ///      "name": "The Club"
    ///    }
    ///  }
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Accept"/>
    public class AcceptActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string AcceptActivtyTypeString = "Accept";

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptActivity"/> class.
        /// </summary>
        public AcceptActivity() : base(type: AcceptActivtyTypeString) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptActivity"/> class.
        /// </summary>
        /// <param name="type">type</param>
        protected AcceptActivity(string type) : base(type) { }
    }
}
