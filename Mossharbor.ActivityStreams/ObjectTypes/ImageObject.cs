using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string ImageType = "Image";

        public ImageObject() : base(type: ImageType) { }
    }
}
