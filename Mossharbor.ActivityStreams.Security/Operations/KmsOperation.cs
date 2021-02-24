using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// KmsOperation
    /// </summary>
    public class KmsOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string KmsOperationType="KmsOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public KmsOperation() : base(KmsOperation.KmsOperationType) { }
    }
}
