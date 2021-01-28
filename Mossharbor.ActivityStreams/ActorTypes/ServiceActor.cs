using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a formal or informal collective of Actors.
    /// </summary>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Service",
    ///  ""name": "Acme Web Service"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Service"/>
    public class ServiceActor : BaseActor
    {
        public ServiceActor() : base(type: "Service") { }
    }
}
