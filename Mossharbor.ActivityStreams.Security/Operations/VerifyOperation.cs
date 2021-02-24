using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// VerifyOperation
    /// </summary>
    public class VerifyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string VerifyOperationType="VerifyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public VerifyOperation() : base(VerifyOperation.VerifyOperationType) { }
    }
}
