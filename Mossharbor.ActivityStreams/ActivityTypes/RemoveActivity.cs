namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is removing the object. If specified, the origin indicates the context from which the object is being removed.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally removed a note from her notes folder",
    ///  "type": "Remove",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": "http://example.org/notes/1",
    ///  "target": {
    ///    "type": "Collection",
    ///    "name": "Notes Folder"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Remove"/>
    public class RemoveActivity : Activity
    {
        public RemoveActivity() : base(type: "Remove") { }
    }
}
