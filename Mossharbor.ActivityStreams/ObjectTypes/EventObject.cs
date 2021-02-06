using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents any kind of event.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Event",
    ///  "name": "Going-Away Party for Jim",
    ///  "startTime": "2014-12-31T23:00:00-08:00",
    ///  "endTime": "2015-01-01T06:00:00-08:00"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Event"/>
    public class EventObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string EventType = "Event";

        public EventObject() : base(type: "EventType") { }
    }
}
