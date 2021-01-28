using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has deleted the object. If specified, the origin indicates the context from which the object was deleted.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally deleted a note",
    ///  "type": "Delete",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": "http://example.org/notes/1",
    ///  "origin": {
    ///    "type": "Collection",
    ///    "name": "Sally's Notes"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Delete"/>
    public class DeleteActivity : Activity
    {
        public DeleteActivity() : base(type: "Delete") { }
    }
}
