﻿using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    ///  Indicates that the actor is calling the target's attention the object.
    /// The origin typically has no defined meaning.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally announced that she had arrived at work",
    ///  "type": "Announce",
    ///  "actor": {
    ///    "type": "Person",
    ///    "id": "http://sally.example.org",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Arrive",
    ///    "actor": "http://sally.example.org",
    ///    "location": {
    ///        "type": "Place",
    ///      "name": "Work"
    ///    }
    ///}
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Announce"/>
    public class AnnounceActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Announce";

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnounceActivity"/> class.
        /// </summary>
        public AnnounceActivity() : base(type: TypeString) { }
    }
}
