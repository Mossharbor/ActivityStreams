using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has updated the object. Note, however, that this vocabulary 
    /// does not define a mechanism for describing the actual set of modifications made to object.
    /// The target and origin typically have no defined meaning
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally updated her note",
    ///  "type": "Update",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": "http://example.org/notes/1"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Update"/>
    public class UpdateActivity : Activity
    {
        public UpdateActivity() : base(type: "Update") { }
    }
}
