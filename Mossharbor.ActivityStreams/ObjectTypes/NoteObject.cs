using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string NoteType = "Note";

        public NoteObject() : base(type: NoteType) { }
    }
}
