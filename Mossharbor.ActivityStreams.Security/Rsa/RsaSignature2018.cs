using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// RsaSignature2018
    /// </summary>
    public class RsaSignature2018: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string RsaSignature2018Type="RsaSignature2018";

        /// <summary>
        /// Constructor
        /// </summary>
        public RsaSignature2018() : base(RsaSignature2018.RsaSignature2018Type) { }
    }
}
