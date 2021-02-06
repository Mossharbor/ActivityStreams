using System.Text.Json;
using System.Text.Json.Serialization;

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
    public class PlaceObject : ActivityObject, ICustomParser
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string PlaceType = "Place";

        public PlaceObject() : base(type: "Place") { }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("altitude")]
        public double Altitude { get; set; }

        /// <summary>
        /// Indicates the accuracy of position coordinates on a Place objects. Expressed in properties of percentage. e.g. "94.0" means "94.0% accurate".
        /// </summary>
        [JsonPropertyName("accuracy")]
        public double Accuracy { get; set; }

        /// <summary>
        /// The radius from the given latitude and longitude for a Place. 
        /// The units is expressed by the units property. If units is not specified, the default is assumed to be "m" indicating "meters".
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Place",
        ///  "name": "Fresno Area",
        ///  "latitude": 36.75,
        ///  "longitude": 119.7667,
        ///  "radius": 15,
        ///  "units": "miles"
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#radius"/>
        [JsonPropertyName("radius")]
        public double Radius { get; set; }

        [JsonPropertyName("units")]
        public string Units { get; set; }

        /// <summary>
        /// Parses out the details specific to the Place Object
        /// </summary>
        /// <param name="el"></param>
        public void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            this.Longitude = el.GetDoubleOrDefault("longitude");
            this.Latitude = el.GetDoubleOrDefault("latitude");
            this.Altitude = el.GetDoubleOrDefault("altitude");
            this.Radius = el.GetDoubleOrDefault("radius");
            this.Units = el.GetStringOrDefault("units");
            this.Accuracy = el.GetDoubleOrDefault("accuracy");

            // If units is not specified, the default is assumed to be "m" indicating "meters".
            if ((this.Radius != double.NaN || this.Altitude != double.NaN) && string.IsNullOrEmpty(this.Units))
                this.Units = "m";
        }
    }
}
