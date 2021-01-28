using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// An image document of any kind
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Image",
    ///  "name": "Cat Jumping on Wagon",
    ///  "url": [
    ///    {
    ///      "type": "Link",
    ///      "href": "http://example.org/image.jpeg",
    ///      "mediaType": "image/jpeg"
    ///    },
    ///    {
    ///    "type": "Link",
    ///      "href": "http://example.org/image.png",
    ///      "mediaType": "image/png"
    ///    }
    ///  ]
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Image"/>
    public class ImageObject : ActivityObject
    {
        public ImageObject() : base(type: "Image") { }
    }
}
