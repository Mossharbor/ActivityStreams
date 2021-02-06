using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{

    [Serializable]
    public class UnknownActivityTypeException : Exception
    {
        public UnknownActivityTypeException() { }
        public UnknownActivityTypeException(string message) : base(message) { }
        public UnknownActivityTypeException(string message, Exception inner) : base(message, inner) { }
        protected UnknownActivityTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
