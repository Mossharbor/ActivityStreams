using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a document of any kind.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Document",
    ///  "name": "4Q Sales Forecast",
    ///  "url": "http://example.org/4q-sales-forecast.pdf"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Document"/>
    public class DocumentObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string DocumentType = "Document";

        public DocumentObject() : base(type: DocumentType) { }
    }
}
