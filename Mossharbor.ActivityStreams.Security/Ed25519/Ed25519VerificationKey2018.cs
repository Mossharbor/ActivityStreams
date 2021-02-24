using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// Ed25519VerificationKey2018
    /// </summary>
    public class Ed25519VerificationKey2018: KeyBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string Ed25519VerificationKey2018Type="Ed25519VerificationKey2018";

        /// <summary>
        /// Constructor
        /// </summary>
        public Ed25519VerificationKey2018() : base(Ed25519VerificationKey2018.Ed25519VerificationKey2018Type) { }
    }
}
