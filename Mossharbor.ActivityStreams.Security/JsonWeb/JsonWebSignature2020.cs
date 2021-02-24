using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// Ed25519Signature2018
    /// </summary>
    public class JsonWebSignature2020 : ProofBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string JsonWebSignature2020Type = "JsonWebSignature2020";

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonWebSignature2020() : base(JsonWebSignature2020.JsonWebSignature2020Type) { }
    }
}
