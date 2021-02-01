using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a question being asked.Question objects are an extension of IntransitiveActivity.
    /// That is, the Question object is an Activity, but the direct object is the question itself and therefore it would not contain an object property.
    /// Either of the anyOf and oneOf properties may be used to express possible answers, but a Question object must not have both properties.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Question",
    ///  "name": "What is the answer?",
    ///  "oneOf": [
    ///    {
    ///      "type": "Note",
    ///      "name": "Option A"
    ///    },
    ///    {
    ///    "type": "Note",
    ///      "name": "Option B"
    ///    }
    ///  ]
    ///}
    /// </example>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Question",
    ///  "name": "What is the answer?",
    ///  "closed": "2016-05-10T00:00:00Z"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Question"/>
    public class QuestionActivity : IntransitiveActivity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Question";

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionActivity"/> class.
        /// </summary>
        public QuestionActivity() : base(type: TypeString) { }

        /// <summary>
        /// Identifies an exclusive option for a Question. 
        /// Use of oneOf implies that the Question can have only a single answer.
        /// To indicate that a Question can have multiple answers, use anyOf.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Question",
        ///  "name": "What is the answer?",
        ///  "oneOf": [
        ///    {
        ///      "type": "Note",
        ///      "name": "Option A"
        ///    },
        ///    {
        ///      "type": "Note",
        ///      "name": "Option B"
        ///    }
        ///  ]
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#oneOf"/>
        [JsonPropertyName("oneOf")]
        public IActivityObjectOrLink[] OneOf { get; set; }

        /// <summary>
        /// Identifies an inclusive option for a Question. 
        /// Use of anyOf implies that the Question can have multiple answers. 
        /// To indicate that a Question can have only one answer, use oneOf.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Question",
        ///  "name": "What is the answer?",
        ///  "anyOf": [
        ///    {
        ///      "type": "Note",
        ///      "name": "Option A"
        ///    },
        ///    {
        ///      "type": "Note",
        ///      "name": "Option B"
        ///    }
        ///  ]
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#anyOf"/>
        [JsonPropertyName("anyOf")]
        public IActivityObjectOrLink[] AnyOf { get; set; }
    }
}
