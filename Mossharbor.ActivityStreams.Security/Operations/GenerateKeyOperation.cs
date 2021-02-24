using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// GenerateKeyOperation
    /// </summary>
    public class GenerateKeyOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string GenerateKeyOperationType="GenerateKeyOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public GenerateKeyOperation() : base(GenerateKeyOperation.GenerateKeyOperationType) { }
    }
}
