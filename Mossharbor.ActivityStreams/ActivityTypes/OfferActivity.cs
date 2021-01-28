using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Indicates that the actor is offering the object. If specified, the target indicates the entity to which the object is being offered.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "summary": "Sally offered 50% off to Lewis",
    ///  "type": "Offer",
    ///  "actor": {
    ///    "type": "Person",
    ///    "name": "Sally"
    ///  },
    ///  "object": {
    ///    "type": "http://www.types.example/ProductOffer",
    ///    "name": "50% Off!"
    ///  },
    ///  "target": {
    ///    "type": "Person",
    ///    "name": "Lewis"
    ///  }
    ///}</example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Offer"/>
    public class OfferActivity : Activity
    {
        public OfferActivity() : base(type: "Offer") { }
    }
}
