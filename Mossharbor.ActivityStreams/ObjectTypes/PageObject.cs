using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string PageType = "Page";

        public PageObject() : base(type: "Page") { }
    }
}
