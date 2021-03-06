﻿using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a formal or informal collective of Actors.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Person",
    ///  "name": "Sally Smith"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Person"/>
    public class PersonActor : BaseActor
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string PersonActorType = "Person";

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonActor"/> class.
        /// </summary>
        public PersonActor() : base(type: "Person") { }
    }
}
