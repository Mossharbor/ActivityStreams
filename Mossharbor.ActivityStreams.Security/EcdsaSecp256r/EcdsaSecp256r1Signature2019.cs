using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// EcdsaSecp256r1Signature2019
    /// </summary>
    public class EcdsaSecp256r1Signature2019: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string EcdsaSecp256r1Signature2019Type="EcdsaSecp256r1Signature2019";

        /// <summary>
        /// Constructor
        /// </summary>
        public EcdsaSecp256r1Signature2019() : base(EcdsaSecp256r1Signature2019.EcdsaSecp256r1Signature2019Type) { }
    }
}
