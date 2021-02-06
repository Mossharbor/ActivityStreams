using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
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
        /// <summary>
        /// the type constant for this Activity
        /// </summary>
        public const string TypeString = "Offer";

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferActivity"/> class.
        /// </summary>
        public OfferActivity() : base(type: TypeString) { }
    }
}
