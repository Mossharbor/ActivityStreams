﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents an organization.
    /// </summary>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Organization",
    ///  name": "Example Co."
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Organization"/>
    public class OrganizationActor : BaseActor
    {
        public OrganizationActor() : base(type: "Organization") { }
    }
}
