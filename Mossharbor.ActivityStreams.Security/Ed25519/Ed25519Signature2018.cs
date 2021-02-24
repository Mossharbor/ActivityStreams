using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// Ed25519Signature2018
    /// </summary>
    public class Ed25519Signature2018: ProofBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string Ed25519Signature2018Type="Ed25519Signature2018";

        /// <summary>
        /// Constructor
        /// </summary>
        public Ed25519Signature2018() : base(Ed25519Signature2018.Ed25519Signature2018Type) { }
    }
}
