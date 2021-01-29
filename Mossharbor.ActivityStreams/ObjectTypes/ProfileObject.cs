using System;
using System.Collections.Generic;
using System.Text;

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
    public class ProfileObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string ProfileType = "Profile";

        public ProfileObject() : base(type: "Profile") { }
    }
}
