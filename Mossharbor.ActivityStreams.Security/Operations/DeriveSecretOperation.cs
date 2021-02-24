using System;
namespace Mossharbor.ActivityStreams.Security
{
    /// <summary>
    /// DeriveSecretOperation
    /// </summary>
    public class DeriveSecretOperation: SecurityObjectBase
    {
        /// <summary>
        /// Type String
        /// </summary>
        public static string DeriveSecretOperationType="DeriveSecretOperation";

        /// <summary>
        /// Constructor
        /// </summary>
        public DeriveSecretOperation() : base(DeriveSecretOperation.DeriveSecretOperationType) { }
    }
}
