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

        [JsonPropertyName("subject")]
        public ActivityObject Subject { get; set; } //TODO this can be an object or a link

        [JsonPropertyName("relationship")]
        public string Relationship { get; set; }

        [JsonPropertyName("object")]
        public ActivityObject Object { get; set; } 
    }
}
