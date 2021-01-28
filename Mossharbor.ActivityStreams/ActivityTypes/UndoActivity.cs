﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is undoing the object. In most cases, the object will be an Activity describing some previously
    /// performed action (for instance, a person may have previously "liked" an article but, for whatever reason, 
    /// might choose to undo that like at some later point in time).
    /// The target and origin typically have no defined meaning
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally retracted her offer to John",
    ///  "type": "Undo",
    ///  "actor": "http://sally.example.org",
    ///  "object": {
    ///    "type": "Offer",
    ///    "actor": "http://sally.example.org",
    ///    "object": "http://example.org/posts/1",
    ///    "target": "http://john.example.org"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Undo"/>
    public class UndoActivity : Activity
    {
        public UndoActivity() : base(type: "Undo") { }
    }
}
