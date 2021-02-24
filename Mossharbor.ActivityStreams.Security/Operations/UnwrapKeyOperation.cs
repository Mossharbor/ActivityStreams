using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// UnwrapKeyOperation
    /// </summary>
    public class UnwrapKeyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string UnwrapKeyOperationType="UnwrapKeyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public UnwrapKeyOperation() : base(UnwrapKeyOperation.UnwrapKeyOperationType) { }
    }
}
