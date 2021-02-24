using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// SignOperation
    /// </summary>
    public class SignOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string SignOperationType="SignOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public SignOperation() : base(SignOperation.SignOperationType) { }
    }
}
