using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is blocking the object. Blocking is a stronger form of Ignore.
    /// The typical use is to support social systems that allow one user to block activities
    /// or content of other users. The target and origin typically have no defined meaning.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally blocked Joe",
    ///  "type": "Block",
    ///  "actor": "http://sally.example.org",
    ///  "object": "http://joe.example.org"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Block"/>
    public class BlockActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Block";

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockActivity"/> class.
        /// </summary>
        public BlockActivity() : base(type: TypeString) { }
    }
}
