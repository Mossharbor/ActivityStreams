using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a Web Page.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Page",
    ///  "name": "Omaha Weather Report",
    ///  "url": "http://example.org/weather-in-omaha.html"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Page"/>
    public class PageObject : ActivityObject
    {
        public PageObject() : base(type: "Page") { }
    }
}
