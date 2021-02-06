using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Represents a formal or informal collective of Actors.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Service",
    ///  ""name": "Acme Web Service"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Service"/>
    public class ServiceActor : BaseActor
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string ServiceActorType = "Service";

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceActor"/> class.
        /// </summary>
        public ServiceActor() : base(type: ServiceActorType) { }
    }
}
