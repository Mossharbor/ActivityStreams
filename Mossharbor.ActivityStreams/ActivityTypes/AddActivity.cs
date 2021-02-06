using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor has added the object to the target. If the target property is not explicitly specified, 
    /// the target would need to be determined implicitly by context. 
    /// The origin can be used to identify the context from which the object originated.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally added a picture of her cat to her cat picture collection",
    ///  "type": "Add",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Image",
    ///    "name": "A picture of my cat",
    ///    "url": "http://example.org/img/cat.png"
    ///  },
    ///  "origin": {
    ///    "type": "Collection",
    ///    "name": "Camera Roll"
    ///  },
    ///  "target": {
    ///    "type": "Collection",
    ///    "name": "My Cat Pictures"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Add"/>
    public class AddActivity :Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Add";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddActivity"/> class.
        /// </summary>
        public AddActivity() : base(type: TypeString) { }
    }
}
