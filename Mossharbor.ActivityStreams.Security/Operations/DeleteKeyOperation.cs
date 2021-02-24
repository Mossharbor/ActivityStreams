using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// DeleteKeyOperation
    /// </summary>
    public class DeleteKeyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string DeleteKeyOperationType="DeleteKeyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteKeyOperation() : base(DeleteKeyOperation.DeleteKeyOperationType) { }
    }
}
