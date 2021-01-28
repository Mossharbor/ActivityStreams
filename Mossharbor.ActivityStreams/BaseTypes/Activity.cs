using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public class Activity : IntransitiveActivity
    {
        public Activity()
        { }

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
