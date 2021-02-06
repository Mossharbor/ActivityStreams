using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    internal class ActivityObjectOrLink : IActivityObjectOrLink
    {
        public IActivityLink Link { get; set; }

        public IActivityObject Obj { get; set; }
    }
}
