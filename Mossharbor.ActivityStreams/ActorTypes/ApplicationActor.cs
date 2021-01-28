﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Describes a software application.
    /// </summary>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Application",
    ///  "name": "Exampletron 3000"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Application"/>
    public class ApplicationActor : BaseActor
    {
        public ApplicationActor() : base(type: "Application") { }
    }
}
