using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// A linked data proof is comprised of information about the proof, parameters required to verify it, and the proof value itself.
    /// </summary>
    /// <see cref="https://w3c-ccg.github.io/ld-proofs/#verification-attributes"/>
    public class KeyBase : SecurityObjectBase, ICustomParser
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string KeyBaseType = "key";

        /// <summary>
        /// Constructor
        /// </summary>
        public KeyBase() : base(KeyBase.KeyBaseType) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public KeyBase(string baseType) : base(baseType) { }

        /// <summary>
        /// The owner of the key
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// A private key PEM property is used to specify the PEM-encoded version of the private key. This encoding is compatible with almost every Secure Sockets Layer library implementation and typically plugs directly into functions intializing private keys.
        /// </summary>
        public string PrivateKeyPem { get; set; }

        /// <summary>
        /// A public key PEM property is used to specify the PEM-encoded version of the public key. This encoding is compatible with almost every Secure Sockets Layer library implementation and typically plugs directly into functions intializing public keys.
        /// </summary>
        public string PublicKeyPem { get; set; }

        /// <summary>
        /// the date the key was created
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// The expiration time is typically associated with a Key and specifies when the validity of the key will expire. It is considered a best practice to only create keys that have very definite expiration periods. This period is typically set to between six months and two years. An digital signature created using an expired key MUST be marked as invalid by any software attempting to verify the signature.
        /// </summary>
        public DateTime? Expires { get; set; }

        /// <summary>
        /// The revocation time is typically associated with a Key that has been marked as invalid as of the date and time associated with the property. Key revocations are often used when a key is compromised, such as the theft of the private key, or during the course of best-practice key rotation schedules.
        /// </summary>
        public DateTime? Revoked { get; set; }

        /// <inheritdoc/>
        public override void PerformCustomParsing(JsonElement el)
        {
            base.PerformCustomParsing(el);

            var type = el.GetStringOrDefault("type");
            if (null == type)
                type = el.GetStringOrDefault("@type");

            var owner = el.GetStringOrDefault("owner");
            var privateKeyPem = el.GetStringOrDefault("privateKeyPem");
            var publicKeyPem = el.GetStringOrDefault("publicKeyPem");
            var expires = el.GetDateTimeOrDefault("expires");
            var created = el.GetDateTimeOrDefault("created");
            var revoked = el.GetDateTimeOrDefault("revoked");

            this.Owner = owner;
            this.PrivateKeyPem = privateKeyPem;
            this.PublicKeyPem = publicKeyPem;
            this.Expires = expires;
            this.Created = created;
            this.Revoked = revoked;
        }
    }
}
