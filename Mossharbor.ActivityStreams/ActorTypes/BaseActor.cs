using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// a base actor class
    /// </summary>
    public class BaseActor : ActivityObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseActor"/> class.
        /// </summary>
        internal BaseActor(string type) : base(type) { }
    }
}
