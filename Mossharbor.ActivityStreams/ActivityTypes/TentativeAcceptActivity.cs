

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A specialization of Accept indicating that the acceptance is tentative.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally tentatively accepted an invitation to a party",
    ///  "type": "TentativeAccept",
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
    /// <see cref="https://www.w3.org/ns/activitystreams#TentativeAccept"/>
    public class TentativeAcceptActivity : AcceptActivity
    {
        public TentativeAcceptActivity() : base(type: "TentativeAccept") { }
    }
}
