using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// the class representing identity proof
    /// </summary>
    /// <example>
    ///{
    ///  "type": "IdentityProof",
    ///  "name": "gargron",
    ///  "signatureAlgorithm": "keybase",
    ///  "signatureValue": "5cfc20c7018f2beefb42a68836da59a792e55daa4d118498c9b1898de7e845690f"
    ///}
    /// </example>
    public class IdentityProof : SignatureBase
    {
        /// <summary>
        /// the type of identity proof
        /// </summary>
        public static string IdentityProofType = "IdentityProof";

        /// <summary>
        /// Contrustor
        /// </summary>
        public IdentityProof() : base(IdentityProofType) { }
    }
}
