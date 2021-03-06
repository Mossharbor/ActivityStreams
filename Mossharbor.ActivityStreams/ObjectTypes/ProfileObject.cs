﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// A Profile is a content object that describes another Object, typically used to describe Actor Type objects. The describes property is used to reference the object being described by the profile.
    /// </summary>
    /// <example>
    /// {
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Profile",
    ///  "summary": "Sally's Profile",
    ///  "describes": {
    ///    "type": "Person",
    ///    "name": "Sally Smith"
    ///  }
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Profile"/>
    public class ProfileObject : ActivityObject, IParsesChildObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string ProfileType = "Profile";

        public ProfileObject() : base(type: "Profile") { }

        /// <summary>
        /// A Profile is a content object that describes another Object,
        /// typically used to describe Actor Type objects. The describes property is used to reference the object being described by the profile.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Profile",
        ///  "summary": "Sally's Profile",
        ///  "describes": {
        ///    "type": "Person",
        ///    "name": "Sally Smith"
        ///  }
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#Profile"/>
        /// <see cref="https://www.w3.org/ns/activitystreams#describes"/>
        [JsonPropertyName("describes")]
        public IActivityObject Describes { get; set; }

        /// <inheritdoc/>
        public override void PerformCustomObjectParsing(JsonElement el, Func<JsonElement, IActivityObject, IActivityObject> activtyObjectParser)
        {
            base.PerformCustomObjectParsing(el, activtyObjectParser);

            if (el.ValueKind == JsonValueKind.Undefined || el.ValueKind == JsonValueKind.Null)
                return;

            var describesProperty = el.GetPropertyOrDefault("describes");

            this.Describes = activtyObjectParser(describesProperty, this);
        }
    }
}
