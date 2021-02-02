using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class IntransitiveActivity : ActivityObject, IParsesChildObjectOrLinks
    {
        public IntransitiveActivity() { }

        public IntransitiveActivity(string type) { this.Type = type; }

        /// <summary>
        /// Describes one or more entities that either performed or are expected to perform the activity. 
        /// Any single activity can have multiple actors. The actor may be specified using an indirect Link.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally offered the Foo object",
        ///  "type": "Offer",
        ///  "actor": "http://sally.example.org",
        ///  "object": "http://example.org/foo"
        ///}
        /// </example>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally offered the Foo object",
        ///  "type": "Offer",
        ///  "actor": {
        ///    "type": "Person",
        ///    "id": "http://sally.example.org",
        ///    "summary": "Sally"
        ///  },
        ///  "object": "http://example.org/foo"
        ///}
        /// </example>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally and Joe offered the Foo object",
        ///  "type": "Offer",
        ///  "actor": [
        ///    "http://joe.example.org",
        ///    {
        ///      "type": "Person",
        ///      "id": "http://sally.example.org",
        ///      "name": "Sally"
        ///    }
        ///  ],
        ///  "object": "http://example.org/foo"
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#actor"/>
        [JsonPropertyName("actor")]
        public IActivityObjectOrLink[] Actor { get; set; }

        /// <summary>
        ///Describes the indirect object, or target, of the activity. The precise meaning of the target
        ///is largely dependent on the type of action being described but will often be the object
        ///of the English preposition "to". For instance, in the activity "John added a movie to his wishlist",
        ///the target of the activity is John's wishlist. An activity can have more than one target. 
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally offered the post to John",
        ///  "type": "Offer",
        ///  "actor": "http://sally.example.org",
        ///  "object": "http://example.org/posts/1",
        ///  "target": "http://john.example.org"
        ///}
        /// </example>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally offered the post to John",
        ///  "type": "Offer",
        ///  "actor": "http://sally.example.org",
        ///  "object": "http://example.org/posts/1",
        ///  "target": {
        ///    "type": "Person",
        ///    "name": "John"
        ///  }
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#target"/>
        [JsonPropertyName("target")]
        public IActivityObjectOrLink[] Target { get; set; }

        /// <summary>
        /// Describes the result of the activity. For instance, if a particular action results in the creation of a new resource, the result property can be used to describe that new resource.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally checked that her flight was on time",
        ///  "type": ["Activity", "http://www.verbs.example/Check"],
        ///  "actor": "http://sally.example.org",
        ///  "object": "http://example.org/flights/1",
        ///  "result": {
        ///    "type": "http://www.types.example/flightstatus",
        ///    "name": "On Time"
        ///  }
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#result"/>
        [JsonPropertyName("result")]
        public IActivityObjectOrLink Result { get; set; }

        /// <summary>
        /// Describes an indirect object of the activity from which the activity is directed. 
        /// The precise meaning of the origin is the object of the English preposition "from". 
        /// For instance, in the activity "John moved an item to List B from List A", the origin of the activity is "List A".
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally moved a post from List A to List B",
        ///  "type": "Move",
        ///  "actor": "http://sally.example.org",
        ///  "object": "http://example.org/posts/1",
        ///  "target": {
        ///    "type": "Collection",
        ///    "name": "List B"
        ///  },
        ///  "origin": {
        ///    "type": "Collection",
        ///    "name": "List A"
        ///  }
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#origin"/>
        [JsonPropertyName("origin")]
        public IActivityObjectOrLink Origin { get; set; }

        /// <summary>
        /// Identifies one or more objects used (or to be used) in the completion of an Activity.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally listened to a piece of music on the Acme Music Service",
        ///  "type": "Listen",
        ///  "actor": {
        ///    "type": "Person",
        ///    "name": "Sally"
        ///  },
        ///  "object": "http://example.org/foo.mp3",
        ///  "instrument": {
        ///    "type": "Service",
        ///    "name": "Acme Music Service"
        ///  }
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#instrument"/>
        [JsonPropertyName("instrument")]
        public IActivityObjectOrLink Instrument { get; set; }

        public void PerformCustomObjectOrLinkParsing(JsonElement el, Func<JsonElement, IActivityObjectOrLink[]> activtyOrLinkObjectParser)
        {
            if (el.TryGetProperty("actor", out JsonElement actorEl))
            {
                this.Actor = activtyOrLinkObjectParser(actorEl);
            }

            if (el.TryGetProperty("origin", out JsonElement originEl))
            {
                this.Origin = activtyOrLinkObjectParser(originEl).FirstOrDefault();
            }

            if (el.TryGetProperty("target", out JsonElement targetEl))
            {
                this.Target = activtyOrLinkObjectParser(targetEl);
            }

            if (el.TryGetProperty("result", out JsonElement resultEl))
            {
                this.Result = activtyOrLinkObjectParser(resultEl).FirstOrDefault();
            }

            if (el.TryGetProperty("instrument", out JsonElement instrumentEl))
            {
                this.Instrument = activtyOrLinkObjectParser(instrumentEl).FirstOrDefault();
            }
        }
    }
}
