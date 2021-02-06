using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{

    [Serializable]
    public class BuildViolationsException : Exception
    {
        public BuildViolationsException(IEnumerable<string> violations) { }
        public BuildViolationsException(string message) : base(message) { }
        public BuildViolationsException(string message, Exception inner) : base(message, inner) { }
        protected BuildViolationsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
