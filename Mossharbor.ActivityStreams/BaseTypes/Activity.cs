using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// the base activity
    /// </summary>
    public class Activity : IntransitiveActivity
    {
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
        public IActivityObject Object { get; set; }
    }
}
