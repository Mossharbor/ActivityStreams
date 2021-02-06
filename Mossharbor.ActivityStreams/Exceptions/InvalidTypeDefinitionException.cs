using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{

    [Serializable]
    public class InvalidTypeDefinitionException : Exception
    {
        public InvalidTypeDefinitionException() { }
        public InvalidTypeDefinitionException(string message) : base(message) { }
        public InvalidTypeDefinitionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidTypeDefinitionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
