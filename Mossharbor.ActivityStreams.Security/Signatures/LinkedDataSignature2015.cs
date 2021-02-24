using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// A Linked Data signature is used for digital signatures on RDF Datasets. The default canonicalization mechanism is specified in the RDF Dataset Normalization specification, which effectively deterministically names all unnamed nodes. The default signature mechanism uses a SHA-256 digest and RSA to perform the digital signature. This signature uses a algorithm for producing the data that it signs and verifies that is different from other Linked Data signatures.
    /// </summary>
    public class LinkedDataSignature2015 : SignatureBase
    {
        /// <summary>
        ///  the type
        /// </summary>
        public static string LinkedDataSignature2015Type = "LinkedDataSignature2015";

        /// <summary>
        /// the constructor
        /// </summary>
        public LinkedDataSignature2015() : base(LinkedDataSignature2015Type, "URDNA2015", "rsa-sha256", "sha256") { }
    }
}
