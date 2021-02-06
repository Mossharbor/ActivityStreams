using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// Describes a software application.
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Application",
    ///  "name": "Exampletron 3000"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Application"/>
    public class ApplicationActor : BaseActor
    {
        /// <summary>
        /// the type constant for this actor
        /// </summary>
        public const string ApplicationActorType = "Application";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationActor"/> class.
        /// </summary>
        public ApplicationActor() : base(type: ApplicationActorType) { }
    }
}
