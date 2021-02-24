using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// RevokeKeyOperation
    /// </summary>
    public class RevokeKeyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string RevokeKeyOperationType="RevokeKeyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public RevokeKeyOperation() : base(RevokeKeyOperation.RevokeKeyOperationType) { }
    }
}
