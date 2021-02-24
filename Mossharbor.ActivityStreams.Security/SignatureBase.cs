using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.Security
{
    
    public class SignatureBase: SecurityObjectBase, ICustomParser
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string SignatureBaseType = "signature";

        /// <summary>
        /// Constructor
        /// </summary>
        public SignatureBase() : base(SignatureBase.SignatureBaseType) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public SignatureBase(string baseType) : base(baseType) { }


        /// <summary>
        /// Constructor
        /// </summary>
        internal SignatureBase(string baseType, string canonicalizationAlgorithm, string signatureAlgorithm, string digestAlgorithm) : base(baseType)
        {
            this.CanonicalizationAlgorithm = canonicalizationAlgorithm;
            this.SignatureAlgorithm = signatureAlgorithm;
            this.DigestAlgorithm = digestAlgorithm;
        }

        /// <summary>
        /// The type of signature
        /// </summary>
        public string SignatureType { get; set; }

        /// <summary>
        /// The creator of the signature
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// The value of the digest
        /// </summary>
        public string SignatureValue { get; set; }

        /// <summary>
        /// The date the signature was created.
        /// </summary>
        public DateTime? Created{ get; set; }

        /// <summary>
        /// The canonicalization algorithm is used to transform the input data into a form that can be passed to a cryptographic digest method. The digest is then digitally signed using a digital signature algorithm. Canonicalization ensures that a piece of software that is generating a digital signature is able to do so on the same set of information in a deterministic manner.
        /// </summary>
        /// <see cref="https://w3c-ccg.github.io/security-vocab/#canonicalizationAlgorithm"/>
        public string CanonicalizationAlgorithm { get; set; }
        /// <summary>
        /// The digest algorithm is used to specify the cryptographic function to use when generating the data to be digitally signed. Typically, data that is to be signed goes through three steps: 1) canonicalization, 2) digest, and 3) signature. This property is used to specify the algorithm that should be used for step #2. A signature class typically specifies a default digest method, so this property is typically used to specify information for a signature algorithm.
        /// </summary>
        /// <see cref="https://w3c-ccg.github.io/security-vocab/#digestAlgorithm"/>
        public string DigestAlgorithm { get; set; }
        /// <summary>
        /// The signature algorithm is used to specify the cryptographic signature function to use when digitally signing the digest data. Typically, text to be signed goes through three steps: 1) canonicalization, 2) digest, and 3) signature. This property is used to specify the algorithm that should be used for step #3. A signature class typically specifies a default signature algorithm, so this property rarely needs to be used in practice when specifying digital signatures.
        /// </summary>
        /// <see cref="https://w3c-ccg.github.io/security-vocab/#signatureAlgorithm"/>
        public string SignatureAlgorithm { get; set; }

        /// <summary>
        /// This property is used in conjunction with the input to the signature hashing function in order to protect against replay attacks. Typically, receivers need to track all nonce values used within a certain time period in order to ensure that an attacker cannot merely re-send a compromised packet in order to execute a privileged request.
        /// </summary>
        public string Nonce { get; set; }

        /// <inheritdoc/>
        public override void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            var type = el.GetStringOrDefault("type");
            if (null == type)
                type = el.GetStringOrDefault("@type");
            var creator = el.GetStringOrDefault("creator");
            var created = el.GetDateTimeOrDefault("created");
            var signatureValue = el.GetStringOrDefault("signatureValue");
            var canonicalizationAlgorithm = el.GetStringOrDefault("canonicalizationAlgorithm");
            var digestAlgorithm = el.GetStringOrDefault("digestAlgorithm");
            var signatureAlgorithm = el.GetStringOrDefault("signatureAlgorithm");
            var nonce = el.GetStringOrDefault("nonce");

            this.Created = created;
            this.SignatureValue = signatureValue;
            this.SignatureType = type;
            this.Creator = creator;
            this.Nonce = nonce;

            if (string.IsNullOrEmpty(this.CanonicalizationAlgorithm))
                this.CanonicalizationAlgorithm = canonicalizationAlgorithm;
            if (string.IsNullOrEmpty(this.DigestAlgorithm))
                this.DigestAlgorithm = digestAlgorithm;
            if (string.IsNullOrEmpty(this.SignatureAlgorithm))
                this.SignatureAlgorithm = signatureAlgorithm;
            
        }
    }
}
