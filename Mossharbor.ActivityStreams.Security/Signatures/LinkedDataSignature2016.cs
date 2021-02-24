using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// A Linked Data signature is used for digital signatures on RDF Datasets. The default canonicalization mechanism is specified in the RDF Dataset Normalization specification, which effectively deterministically names all unnamed nodes. The default signature mechanism uses a SHA-256 digest and RSA to perform the digital signature.
    /// </summary>
    public class LinkedDataSignature2016 : SignatureBase
    {
        /// <summary>
        ///  the type
        /// </summary>
        public static string LinkedDataSignature2016Type = "LinkedDataSignature2016";

        /// <summary>
        /// the constructor
        /// </summary>
        public LinkedDataSignature2016() : base(LinkedDataSignature2016Type, "URDNA2015", "rsa-sha256", "sha256") { }
    }
}
