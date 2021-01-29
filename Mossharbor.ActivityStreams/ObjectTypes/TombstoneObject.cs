
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A Tombstone represents a content object that has been deleted. It can be used in Collections to signify that there used to be an object at this position, but it has been deleted.
    /// </summary>
    /// <example>
    ///{
    ///  "type": "OrderedCollection",
    ///  "totalItems": 3,
    ///  "name": "Vacation photos 2016",
    ///  "orderedItems": [
    ///    {
    ///      "type": "Image",
    ///      "id": "http://image.example/1"
    ///    },
    ///    {
    ///    "type": "Tombstone",
    ///      "formerType": "Image",
    ///      "id": "http://image.example/2",
    ///      "deleted": "2016-03-17T00:00:00Z"
    ///    },
    ///    {
    ///    "type": "Image",
    ///      "id": "http://image.example/3"
    ///    }
    ///  ]
    ///}
    /// </example>
    /// <see cref="	https://www.w3.org/ns/activitystreams#Tombstone"/>
    public class TombstoneObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string TombstoneType = "Tombstone";

        public TombstoneObject() : base(type: TombstoneType) { }
    }
}
