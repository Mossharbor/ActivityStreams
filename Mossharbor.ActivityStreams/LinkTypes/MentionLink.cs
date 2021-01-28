using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A specialized Link that represents an @mention.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Mention of Joe by Carrie in her note",
    ///  "type": "Mention",
    ///  "href": "http://example.org/joe",
    ///  "name": "Joe"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Mention"/>
    public class MentionLink : ActivityLink
    {
        public MentionLink() : base(type: "Mention") { }
    }
}
