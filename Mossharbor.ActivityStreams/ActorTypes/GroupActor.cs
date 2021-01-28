using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a formal or informal collective of Actors.
    /// </summary>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Group",
    ///  "name": "Big Beards of Austin"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Group"/>
    public class GroupActor : BaseActor
    {
        public GroupActor() : base(type: "Group") { }
    }
}
