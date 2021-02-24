using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// ExportKeyOperation
    /// </summary>
    public class ExportKeyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string ExportKeyOperationType="ExportKeyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public ExportKeyOperation() : base(ExportKeyOperation.ExportKeyOperationType) { }
    }
}
