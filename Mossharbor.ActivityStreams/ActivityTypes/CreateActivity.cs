using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has created the object.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally created a note",
    ///  "type": "Create",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Note",
    ///    "name": "A Simple Note",
    ///    "content": "This is a simple note"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Create"/>
    public class CreateActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Create";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivity"/> class.
        /// </summary>
        public CreateActivity() : base(type: TypeString) { }
    }
}
