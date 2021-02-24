using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// WrapKeyOperation
    /// </summary>
    public class WrapKeyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string WrapKeyOperationType="WrapKeyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public WrapKeyOperation() : base(WrapKeyOperation.WrapKeyOperationType) { }
    }
}
