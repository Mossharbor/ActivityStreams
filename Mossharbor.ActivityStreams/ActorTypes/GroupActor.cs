using System;
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
    ///  "type": "Group",
    ///  "name": "Big Beards of Austin"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Group"/>
    public class GroupActor : BaseActor
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string GroupActorType = "Group";

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupActor"/> class.
        /// </summary>
        public GroupActor() : base(type: "Group") { }
    }
}
