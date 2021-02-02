using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    ///  Describes a relationship between two individuals.The subject and object properties are used to identify the connected individuals.
    /// See 5.2 Representing Relationships Between Entities for additional information.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally is an acquaintance of John",
    ///  "type": "Relationship",
    ///  "subject": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "relationship": "http://purl.org/vocab/relationship/acquaintanceOf",
    ///  "object": {
    ///    "type": "Person",
    ///    "name": "John"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Relationship"/>
    public class RelationshipObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string RelationshipType = "Relationship";

        public RelationshipObject() : base(type: RelationshipType) { }

        /// <summary>
        /// 	On a Relationship object, the subject property identifies one of the connected individuals. For instance, for a Relationship object describing "John is related to Sally", subject would refer to John.
        /// </summary>
        [JsonPropertyName("subject")]
        public IActivityObjectOrLink[] Subject { get; set; } //TODO this can be an object or a link

        /// <summary>
        /// On a Relationship object, the relationship property identifies the kind of relationship that exists between subject and object.
        /// </summary>
        [JsonPropertyName("relationship")]
        public string Relationship { get; set; }

        /// <summary>
        /// Describes an object of any kind. The Object type serves as the base type for most of the other kinds of objects defined in the Activity Vocabulary,
        /// including other Core types such as Activity, IntransitiveActivity, Collection and OrderedCollection.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#Object"/>
        [JsonPropertyName("object")]
        public IActivityObject Object { get; set; }
    }
}
