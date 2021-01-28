using System;
using System.Collections.Generic;
using System.Text;

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
        /// Initializes a new instance of the <see cref="QuestionActivity"/> class.
        /// </summary>
        public QuestionActivity() : base(type: "Question") { }
    }
}
