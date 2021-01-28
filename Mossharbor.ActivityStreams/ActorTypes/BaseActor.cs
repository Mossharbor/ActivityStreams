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
        internal BaseActor(string type) : base(type) { }
    }
}
