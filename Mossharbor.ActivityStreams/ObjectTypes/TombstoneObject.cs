
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
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
    public class TombstoneObject : ActivityObject, ICustomParser
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string TombstoneType = "Tombstone";

        public TombstoneObject() : base(type: TombstoneType) { }

        /// <summary>
        /// On a Tombstone object, the formerType property identifies the type of the object that was deleted.
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
        [JsonPropertyName("formerType")]
        public string FormerType { get; set; }

        /// <summary>
        /// On a Tombstone object, the deleted property is a timestamp for when the object was deleted.
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
        [JsonPropertyName("deleted")]
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// Parses out the details specific to the Place Object
        /// </summary>
        /// <param name="el"></param>
        public override void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            string formerType = el.GetStringOrDefault("formerType");
            var deleted = el.GetDateTimeOrDefault("deleted");
            this.FormerType = formerType;
            this.Deleted = deleted;
        }
    }
}
