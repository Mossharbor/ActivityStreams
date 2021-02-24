using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// This class represents a message digest that may be used for data integrity verification. The digest algorithm used will determine the cryptographic properties of the digest.
    /// </summary>
    /// <example>
    ///    {
    ///  "@context": "https://w3id.org/security/v1",
    ///  "@type": "Digest",
    ///  "digestAlgorithm": "http://www.w3.org/2000/09/xmldsig#sha1",
    ///  "digestValue": "981ec496092bf6ee18d6255d96069b528633268b"
    ///}
    ///</example>
    public class Digest : SecurityObjectBase, ICustomParser
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string DigestType = "Digest";

        /// <summary>
        /// Constructor
        /// </summary>
        public Digest() : base(Digest.DigestType) { }

        /// <summary>
        /// The digest algorithm is used to specify the cryptographic function to use when generating the data to be digitally signed. Typically, data that is to be signed goes through three steps: 1) canonicalization, 2) digest, and 3) signature. This property is used to specify the algorithm that should be used for step #2. A signature class typically specifies a default digest method, so this property is typically used to specify information for a signature algorithm.
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// The digest value is used to express the output of the digest algorithm expressed in Base-16 (hexadecimal) format.
        /// </summary>
        public string DigestValue { get; set; }

        /// <inheritdoc/>
        public override void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            var algorithm = el.GetStringOrDefault("digestAlgorithm");
            var value = el.GetStringOrDefault("digestValue");

            this.Algorithm = algorithm;
            this.DigestValue = value;
        }
    }
}
