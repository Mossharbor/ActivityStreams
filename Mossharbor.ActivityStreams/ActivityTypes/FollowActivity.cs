﻿using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is "following" the object. Following is defined in the sense 
    /// typically used within Social systems in which the actor is interested in any 
    /// activity performed by or on the object. The target and origin typically have no defined meaning.
    /// </summary>
    /// <see cref="	https://www.w3.org/ns/activitystreams#Follow"/>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally followed John",
    ///  "type": "Follow",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "Person",
    ///    "name": "John"
    ///  }
    ///}
    /// </example>
    public class FollowActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Follow";

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowActivity"/> class.
        /// </summary>
        public FollowActivity() : base(type: TypeString) { }
    }
}
