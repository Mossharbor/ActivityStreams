using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// The Place object is used to represent both physical and logical locations. 
    /// While numerous existing vocabularies exist for describing locations in a variety of ways,
    /// inconsistencies and incompatibilities between those vocabularies make it difficult to achieve 
    /// appropriate interoperability between implementations. The Place object is included within 
    /// the Activity vocabulary to provide a minimal, interoperable starting point for describing
    /// locations consistently across Activity Streams 2.0 implementations.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Place",
    ///  "name": "Work"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/TR/activitystreams-vocabulary/#places"/>
    /// <see cref="https://www.w3.org/ns/activitystreams#Place"/>
    public class PlaceObject : ActivityObject
    {
        public PlaceObject() : base(type: "Place") { }
    }
}
