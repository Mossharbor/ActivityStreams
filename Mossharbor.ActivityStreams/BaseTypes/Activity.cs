using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// the base activity
    /// </summary>
    public class Activity : IntransitiveActivity, IParsesChildObjects
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string ActivityType = "Activity";

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        public Activity()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        /// <param name="type">the type of the activity</param>
        public Activity(string type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Describes an object of any kind. The Object type serves as the base type for most of the other kinds of objects defined in the Activity Vocabulary,
        /// including other Core types such as Activity, IntransitiveActivity, Collection and OrderedCollection.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#Object"/>
        [JsonPropertyName("object")]
        public IActivityObject[] Object { get; set; }

        /// <inheritdoc/>
        public void PerformCustomObjectParsing(JsonElement el, Func<JsonElement, IActivityObject[]> activtyObjectsParser)
        {
            if (el.TryGetProperty("object", out JsonElement objectEl))
            {
                this.Object = activtyObjectsParser(objectEl);
            }
        }
    }
}
