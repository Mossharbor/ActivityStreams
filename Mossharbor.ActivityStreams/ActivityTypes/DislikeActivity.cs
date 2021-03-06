﻿using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor dislikes the object.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally disliked a post",
    ///  "type": "Dislike",
    ///  "actor": "http://sally.example.org",
    ///  "object": "http://example.org/posts/1"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Dislike"/>
    public class DislikeActivity : Activity
    {
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Dislike";

        /// <summary>
        /// Initializes a new instance of the <see cref="DislikeActivity"/> class.
        /// </summary>
        public DislikeActivity() : base(type: TypeString) { }
    }
}
