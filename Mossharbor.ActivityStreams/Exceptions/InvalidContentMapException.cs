using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{

    [Serializable]
    public class InvalidContentMapException : Exception
    {
        public InvalidContentMapException() { }
        public InvalidContentMapException(string message) : base(message) { }
        public InvalidContentMapException(string message, Exception inner) : base(message, inner) { }
        protected InvalidContentMapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    
    }
}
