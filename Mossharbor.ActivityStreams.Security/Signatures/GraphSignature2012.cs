using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// A graph signature is used for digital signatures on RDF graphs. 
    /// The default canonicalization mechanism is specified in the RDF Graph normalization specification,
    /// which effectively deterministically names all unnamed nodes. 
    /// The default signature mechanism uses a SHA-256 digest and RSA to perform the digital signature.
    /// </summary>
    /// <see cref="https://w3c-ccg.github.io/security-vocab/#GraphSignature2012"/>
    public class GraphSignature2012 : SignatureBase
    {
        /// <summary>
        ///  the type
        /// </summary>
        public static string GraphSignature2012Type = "GraphSignature2012";

        /// <summary>
        /// the constructor
        /// </summary>
        public GraphSignature2012() : base(GraphSignature2012Type, "URGNA2012", "rsa-sha256", "sha256") { }
    }
}
