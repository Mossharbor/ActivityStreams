using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents an organization.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Organization",
    ///  name": "Example Co."
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Organization"/>
    public class OrganizationActor : BaseActor
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string OrganizationActorType = "Organization";

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationActor"/> class.
        /// </summary>
        public OrganizationActor() : base(type: "Organization") { }
    }
}
