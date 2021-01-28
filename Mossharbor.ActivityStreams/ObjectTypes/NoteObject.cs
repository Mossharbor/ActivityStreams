using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a short written work typically less than a single paragraph in length.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Note",
    ///  "name": "A Word of Warning",
    ///  "content": "Looks like it is going to rain today. Bring an umbrella!"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Note"/>
    public class NoteObject : ActivityObject
    {
        public NoteObject() : base(type: "Note") { }
    }
}
