using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// EcdsaSecp256k1Signature2019
    /// </summary>
    public class EcdsaSecp256k1Signature2019: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string EcdsaSecp256k1Signature2019Type="EcdsaSecp256k1Signature2019";

        /// <summary>
        /// Constructor
        /// </summary>
        public EcdsaSecp256k1Signature2019() : base(EcdsaSecp256k1Signature2019.EcdsaSecp256k1Signature2019Type) { }
    }
}
